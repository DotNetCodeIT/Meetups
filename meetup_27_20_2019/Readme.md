# Novità in arrivo dalla .net conf

Ci siamo quasi a fine ottobre, nella [.Net conf del 23-25 ottobre 2019][dot-net-conf] verrà rilasciata in GA la versione finale di __.net code 3__.

> Per poter vedere prima degli altri __.net core 3__ e cominciare ad apprezzare i cambiamenti potete scaricare i pacchetti che servono dalla [pagina ufficiale di .net core][dot-netcode-official-download] oppure più semplicemente passare al canale [preview di Visual studio][vs-preview-channel], _se siete utenti mac o linux di Visual  Studio probabilmente sapete già come passare al canale preview senza bisogno di maggiori informazioni_. Al momento in cui scrivo (fine agosto 2019) è stata rilasciata la __preview 8 di .net core 3__, come sempre non ci sono garanzie sul fatto che tutte le caratteristiche presenti nella preview siano poi effettivamente portate in GA ma data la vicinanza del rilascio sono ragionevolmente confidente.
>
> Vi segnalo inoltre il modulo __[dot net try][dot-net-try]__ per .net a riga di comando che apre una pagina web in cui scrivere codice in c#, utile sia per testare le novità del linguaggio sia per fare dei piccoli test senza creare decine di progetti inutili sulle nostre macchine.

Le novità, al livello di linguaggio che saranno introdotte dalla [.Net conf del 23-25 ottobre 2019][dot-net-conf] saranno principalmente su due fronti:

* Novità di c# versione 8
* .Net Core Versione 3

Come è ovvio le novità di .net core 3 riguarderanno "solo" l'universo creato da .net core mentre le novità introdotte nel linguaggio si rifletteranno anche (a richiesta) sul "vecchio" framework .net.

## Novità di c# 8

Potete trovare tutte le novità su [Novità di c# 8][whats-new-csharp-8] e sulla relativa [repository github][github-try-samples-csharp-8].

> Al momento in cui scrivo è stata rilasciata la versione 5 in preview di c# 8

### Membri in sola lettura

E' stato introdotto il modificatore __readonly__ su qualunque membro di una __struct__.
All'interno di un metodo marcato come readonly non è possibile modificare il contenuto di un field perchè genererebbe un errore di compilazione. Anche il richiamare un metodo non readonly genera un warning.
Questo rende più difensivo il nostro codice anche se, purtroppo, questa caratteristica è limitata alle sole __struct__ e non alle classi.

```c#
    public struct PointNew
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Distance => Math.Sqrt(X * X + Y * Y);

        public readonly override string ToString()
        {
            // Errore di compilazione
            // X = 10;

            // Dinstance non è readonly e genera un warning
            return $"({X}, {Y}) is {Distance} from the origin";
        }

        public readonly void Translate(int xOffset, int yOffset)
        {
            // Errore di compilazione
            // X += xOffset;
            // Y += yOffset;
        }
    }
```

### Modifiche alle interfacce

Sono state introdotte varie modifiche alle interfacce in c# 8 in particolare sono stati introdotti i metodi di default nell'interfaccia e, quasi come conseguenza naturale, la possibilità di inserire metodi statici nelle interfacce.

> A me fa parecchio strano scrivere __definizione di metodi nell'interfaccia__

#### Membri di interfacce predefiniti

E' Possibile "definire nell'interfaccia" dei metodi di default. _Questo rende le interfacce un pochino più simili alle classi astratte._

> Questa nuova funzionalità è molto utile se si vuole garantire la retro compatibilità con le versioni precedenti di una certa api.

```c#
    public interface IDefaultMember
    {
        void DefaultMethod(string str)
        {
            Console.WriteLine($"Hello wold {str} [Default version]");
        }
    }
```

Ipotiziamo di aver definito due classi concrete DefaultMember (che non rifefinisce DefaultMethod) e FullImplementation (che al contrario lo ridefinisce)

```c#
    // DefaultMember non contiene la ridefinizione di DefaultMethod
    IDefaultMember defaultMemberInterface = new DefaultMember();

    // FullImplementation contiene la ridefinizione di DefaultMethod
    IDefaultMember fullImplementation = new FullImplementation();

    // come intuitiuvamente atteso lanciare questo metodo equivale a lanciare il metodo di default "definito nell'interfaccia" (fa ancora strano scriverlo)
    defaultMemberInterface.DefaultMethod("ciao mondo-default");

    // fullimplementation invece rifedinisce il metodo DefaultMethod e quindi viene lanciata la ridefinizione di DefaultMethod
    fullImplementation.DefaultMethod("ciao mondo-ridefinita");
```

#### Membri statici di interfaccie

È possibile definire dei membri statici di interfacce che varranno per tutte le classi derivate.

```c#
    public interface IStaticMethodInterface
    {
        static void ChangeHello(string hello)
        {
            helloString = hello;
        }

        static string helloString = "Hello world";

        void PrintHello()
        {
            Console.WriteLine(helloString);
        }
    }
```

Ipotizzando di avere due classi concrete in cui __non__ è stata ridefinita PrintHello()

```c#
    IStaticMethodInterface staticDefault = new StaticMethodInterfaceDefault();
    IStaticMethodInterface staticImplementation = new StaticMethodInterfaceOther();

    staticDefault.PrintHello(); // Hello world
    staticImplementation.PrintHello(); // Hello world

    IStaticMethodInterface.ChangeHello("Ciao mondo");

    staticDefault.PrintHello(); // Ciao mondo
    staticImplementation.PrintHello(); // Ciao mondo
```

> Combinando i metodi statici definiti nelle interfacce e le implementazioni di default dei metodi è possibile [Estendere l'implementazione predefinita][extend-the-default-implementation]

## Più pattern per l'operatore switch

Questa feature è la mia preferita perché presente anche in altri linguaggi moderni come swift.

> La documentazione recita _questa feature rappresenta il primo tentativo andare verso un paradigma che divida dati e funzionalità_ e noi non possiamo che esserne felici.

### Switch expression

Le espressioni switch sono il naturale proseguimento dei metodi "senza corpo" o meglio delle [Expression body definition][expression-body-definition].

Questa caratteristica rende notevolmente più snello e leggibile il codice di alcuni tipi di switch.

```c#
  private static string PatternMatchingSwitch(LoggingLevel loggingLevel)
    => loggingLevel switch
    {

        LoggingLevel.Alert => "Alert",
        LoggingLevel.Warning => "Warning",
        LoggingLevel.Info => "Info",
        LoggingLevel.Debug => "Debug",
        _ => "Other"
    };
```

notare l'inserimento di _ al posto di __default__

Le _switch expression_ risultano, come è ovvio particolarmente comode in tutte quelle situazioni in cui è necessario mappare dei valori.

### Property patterns

Il property pattern è di gran lunga la feature nuova di c# 8 che preferisco perchè fin da quando è uscita su swift la ho sempre invidiata parecchio.

in sostanza questa nuova caratteristica consente (qui scritta in forma compatta) consente di usare una classe in un istruzione di switch e di utilizzare come filtyro contemporaneamente più field della classe.

L'esempio qui sotto è di facile interpretazione: dato un oggetti City che contiene sia il nome di uno stato che il nome di una città la funzione qui ssotto estrare una label.
Questo esempio, di scarso significato pratico, serve solo a dimostrare quanto sia facile con c# 8 fare pattern matching su oggetti complessi senza far ricorso a if nei rami dello switch.

```c#
    private string GetStateName(City address)
        => address switch
        {
            { Country: "IT", Name: "Rome" } => "Italy (Rome)",
            { Country: _, Name:"Paris" } => "France or Texas",
            { Country: "IT",Name: _} => "Italy",
            _ => "Other"
        };
```

__Con il _Property pattern_ è possibile usare la forma estesa, e più familiare, dello switch; lo trovate nella repository di gitbhub allegata__

### Tuple pattern

Di significato molto simile al property pattern è possibile usare le Tuple come argomento dello switch

```c#
    public string RockPaperScissors(string country, string city)
        => (country, city) switch
        {
            ("IT", "Rome") => "Italy (Rome)",
            (_, "Paris") => "France or Texas",
            ("IT", _) => "Italy",
            _ => "Other", // Forma compatta per ( _, _) => "Other"
        };
```

### Positional Pattern

E' stato introdotto il Decostruttore per alcuni tipi.
Il Decostruttore di suo non è molto utile perchè "appiattisce" un oggetto in una lista di parametri.

```c#
    public class PointModel
    {

        public int X { get; }
        public int Y { get; }

        public PointModel(int x, int y) => (X, Y) = (x, y);

        public void Deconstruct(out int x, out int y) =>
            (x, y) = (X, Y);
    }
```

Di suo non è un costrutto molto utile, si puo' già fare con le vecchie versioni di c# usando una nomenclatura "non standard".
Accoppiato con la clausula _when_ degli switch invece risulta particolarmente efficace.

In questo esempio possiamo vedere quanto sia facile mappare un punto in un piano cartesiano per capire su quale quadrante è  posizionato.
Il pointModel viene Decostruito ed iserito automaticamente in una tupla, poi viene usata la clausula when.

> La clausula _when_ più che l'estenzione dello switch possiamo vederlo come un modo per rendere più elegante _else if_

```c#
    private string WhenClause() => pointModel switch
    {
        (0, 0) => "origin",
        var (x, y) when x > 0 && y > 0 => "dx-up",
        var (x, y) when x < 0 && y > 0 => "sx-up",
        var (x, y) when x < 0 && y < 0 => "sx-dw",
        var (x, y) when x > 0 && y < 0 => "dx-dw",
        var (_, _) => "border",
        _ => "Unknown"
    };
```

## Using declaration

Con c# 8 le definizioni di using risultano notevolmente semplificate (sopratutto nella lettura)

```c#
    public void Print()
    {
        Console.WriteLine($"pre using declared");
        using var disposableClass = new DisposableClass();
        using var disposableStruct = new DisposableStruct();
        // different from using (var d = new DisposableClass())

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"post using declared ");

        Console.WriteLine($"After this line the objects dispose");
    }
```

Notare il _punto e virgola_ alla fine dello __using__ e che in questa versione lo __using__ non usa le parentesi.
La _disposableClass_ viene rilasciata alla fine del metodo.

> Dal punto di vista pratico se la variabile dichiarada nello using puo' vivere fino alla fine del metodo che lo contiene allora non è più necessario inserire il blocco di codice sotto lo using.

## Funzioni locali statiche

Le funzioni locali statiche sono un'estenzione delle funzioni locali già presenti nel linguaggio

```c#
    public void Print()
    {
        int a = 5;
        int b = 6;

        Console.WriteLine(Sum(a, b));

        static int Sum(int _a, int _b)
        {
            // return a + b; a e b sono inaccessibili perchè la funzione è statica
            return _a + _b;
        }
    }
```

Dal punto di vista pratico se si dichiara una funzione statica locale allora le variabili dichiarate al di fuori del corpo della funzione statica allora queste variabili risultano inaccessibili.



Potete vedere esempi più avanzatati di pattern matching nell'[Esercitazione: Uso di funzionalità di criteri di ricerca per estendere i tipi di dati][tutorials-pattern-matching]

[dot-netcode-official-download]: https://dotnet.microsoft.com/download/dotnet-core/3.0 ".net core official download"

[vs-preview-channel]: https://visualstudio.microsoft.com/vs/preview/ "Visual studio preview chanel"

[dot-net-try]: https://github.com/dotnet/try ".net try"

[dot-net-conf]: https://www.dotnetconf.net/ ".net Conf"
[whats-new-csharp-8]: https://docs.microsoft.com/it-it/dotnet/csharp/whats-new/csharp-8 "News on C# 8"

[github-try-samples-csharp-8]: https://github.com/dotnet/try-samples/tree/master/csharp8 "Esempi di c# 8"

[extend-the-default-implementation]: https://docs.microsoft.com/it-it/dotnet/csharp/tutorials/default-interface-members-versions#extend-the-default-implementation "Default interface member"

[expression-body-definition]:https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-operator#expression-body-definition "Expression body definition"

[tutorials-pattern-matching]: https://docs.microsoft.com/it-it/dotnet/csharp/tutorials/pattern-matching "Esercitazioni sul Pattern matching"
