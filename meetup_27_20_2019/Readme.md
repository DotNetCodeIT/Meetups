# Novità in arrivo dalla .net conf

Ci siamo quasi a fine ottobre, nella [.Net conf del 23-25 ottobre 2019][dot-net-conf] verrà rilasciata in GA la versione finale di __.net core 3__.

> Per poter vedere prima degli altri __.net core 3__ e cominciare ad apprezzare i cambiamenti potete scaricare i pacchetti che servono dalla [pagina ufficiale di .net core][dot-netcode-official-download] oppure più semplicemente passare al canale [preview di Visual studio][vs-preview-channel], _se siete utenti mac o linux di Visual  Studio probabilmente sapete già come passare al canale preview senza bisogno di maggiori informazioni_. Al momento in cui scrivo (fine agosto 2019) è stata rilasciata la __preview 8 di .net core 3__, come sempre non ci sono garanzie sul fatto che tutte le caratteristiche presenti nella preview siano poi effettivamente portate in GA ma data la vicinanza del rilascio sono ragionevolmente confidente.
>
> Vi segnalo inoltre il modulo __[dot net try][dot-net-try]__ per .net a riga di comando che apre una pagina web in cui scrivere codice in C#, utile sia per testare le novità del linguaggio sia per fare dei piccoli test senza creare decine di progetti inutili sulle nostre macchine.

Le novità, al livello di linguaggio che saranno introdotte dalla [.Net conf del 23-25 ottobre 2019][dot-net-conf] saranno principalmente su due fronti:

* Novità di C# versione 8
* .Net Core Versione 3

Come è ovvio le novità di .net core 3 riguarderanno "solo" l'universo creato da .net core mentre le novità introdotte nel linguaggio si rifletteranno anche (a richiesta) sul "vecchio" framework .net.

## Novità di C# 8

Potete trovare tutte le novità su [Novità di C# 8][whats-new-csharp-8] e sulla relativa [repository github][github-try-samples-csharp-8].

> Al momento in cui scrivo è stata rilasciata la versione 5 in preview di C# 8

### Membri in sola lettura

E' stato introdotto il modificatore __readonly__ su qualunque membro di una __struct__.
All'interno di un metodo marcato come readonly non è possibile modificare il contenuto di un field perchè genererebbe un errore di compilazione. Anche il richiamare un metodo non readonly genera un warning.
Questo rende più difensivo il nostro codice anche se, purtroppo, questa caratteristica è limitata alle sole __struct__ e non alle classi.

```C#
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

Sono state introdotte varie modifiche alle interfacce in C# 8 in particolare sono stati introdotti i metodi di default nell'interfaccia e, quasi come conseguenza naturale, la possibilità di inserire metodi statici nelle interfacce.

> A me fa parecchio strano scrivere __definizione di metodi nell'interfaccia__

#### Membri di interfacce predefiniti

E' Possibile "definire nell'interfaccia" dei metodi di default. _Questo rende le interfacce un pochino più simili alle classi astratte._

> Questa nuova funzionalità è molto utile se si vuole garantire la retro compatibilità con le versioni precedenti di una certa api.

```C#
    public interface IDefaultMember
    {
        void DefaultMethod(string str)
        {
            Console.WriteLine($"Hello wold {str} [Default version]");
        }
    }
```

Ipotiziamo di aver definito due classi concrete DefaultMember (che non ridefinisce DefaultMethod) e FullImplementation (che al contrario lo ridefinisce)

```C#
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

```C#
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

```C#
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

```C#
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

Il property pattern è di gran lunga la feature nuova di C# 8 che preferisco perchè fin da quando è uscita su swift la ho sempre invidiata parecchio.

in sostanza questa nuova caratteristica consente (qui scritta in forma compatta) consente di usare una classe in un istruzione di switch e di utilizzare come filtro contemporaneamente più field della classe.

L'esempio qui sotto è di facile interpretazione: dato un oggetti City che contiene sia il nome di uno stato che il nome di una città la funzione qui sotto estrare una label.
Questo esempio, di scarso significato pratico, serve solo a dimostrare quanto sia facile con C# 8 fare pattern matching su oggetti complessi senza far ricorso a if nei rami dello switch.

```C#
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

```C#
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

```C#
    public class PointModel
    {

        public int X { get; }
        public int Y { get; }

        public PointModel(int x, int y) => (X, Y) = (x, y);

        public void Deconstruct(out int x, out int y) =>
            (x, y) = (X, Y);
    }
```

Di suo non è un costrutto molto utile, si puo' già fare con le vecchie versioni di C# usando una nomenclatura "non standard".
Accoppiato con la clausula _when_ degli switch invece risulta particolarmente efficace.

In questo esempio possiamo vedere quanto sia facile mappare un punto in un piano cartesiano per capire su quale quadrante è  posizionato.
Il pointModel viene Decostruito ed iserito automaticamente in una tupla, poi viene usata la clausula when.

> La clausula _when_ più che l'estenzione dello switch possiamo vederlo come un modo per rendere più elegante _else if_

```C#
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

Potete vedere esempi più avanzatati di pattern matching nell'[Esercitazione: Uso di funzionalità di criteri di ricerca per estendere i tipi di dati][tutorials-pattern-matching]

## Using declaration e struct IDisposable

Con C# 8 le definizioni di using risultano notevolmente semplificate (sopratutto nella lettura)

```C#
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

Ulteriore modifica è la possibilità di dichiarare una struttura IDisposable

## Funzioni locali statiche

Le funzioni locali statiche sono un'estenzione delle funzioni locali già presenti nel linguaggio

```C#
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

## Flussi asincroni

Con C# 8 è stato introdotto il supporto ai flussi asinctroni. In sintesi i flussi asinctroni sono metodi async che tornano un oggetto di tipo __`IAsyncEnumerable<T>`__  e che hanno un return espesso con uno __yeld__.

Un esempio semplice di flusso asincrono è:

```C#
    public static async System.Collections.Generic.IAsyncEnumerable<int> GenerateSequence()
    {
        for (int i = 0; i < 20; i++)
        {
            await Task.Delay(100);
            yield return i;
        }
    }
```

questo flusso asincrono puo' essere consumato a partire da un nuovo costrutto __await foreach__ che è molto leggibile rispetto a soluzioni più barocche.

```C#
await foreach (var number in GenerateSequence())
{
    Console.WriteLine(number);
}
```

L'utilizzo di __await foreach__ è stato creato per consumare i flussi asincroni ed è una soluzione particolarmente efficiente nel caso in cui un __Parallel.foreach__ non sia sufficiente in quanto è necessario garantire l'ordine in cui saranno letti gli elementi della lista.

Dal punto di vista pratico se si dichiara una funzione statica locale allora le variabili dichiarate al di fuori del corpo della funzione statica allora queste variabili risultano inaccessibili.

## Indici ed intervalli

Sono stati introdotti gli oggetti System.Index e System.Range

### Indici

In sostanza usando gli indici ora è possibile accedere direttamente agli ultimi elementi di una collezione senza dover necessariamente fare delle conversioni con Lenght - x.

Semplificando, per accedere agli elementi di una lista in ordine cresciente si possono usare gli indici "normalmente" andando da 0 a Lenght -1.

Sono stati introdotti anche gli indici "inversi" che lavorano sugli ultimi elementi che vanno da ^1 a ^Lenght.

* ^1 viene interpretato come Lenght -1
* ^2 viene interpretato come Lenght -2
* ^Lenght viene interpretato come Lenght- Lenght quindi 0
* ^0 viene interprepato come Lenght - 0 quindi Lenght e quindi genera un'eccezione

nello specifico avendo la sequente lista

```c#
    string[] number = new string[]
    {
                    // index from start    index from end
        "zero",     // 0                   ^10 (Lenght)
        "uno",      // 1                   ^9
        "due",      // 2                   ^8
        "tre",      // 3                   ^7
        "quattro",  // 4                   ^6
        "cinque",   // 5                   ^5
        "sei",      // 6                   ^4
        "sette",    // 7                   ^3
        "otto",     // 8                   ^2
        "nove"      // 9 (or words.Length) ^1
    };              // 10(or words.Length) ^0

```

è possibile utilizzare gli indici in questo modo

```c#
    public void PrintIndex()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine($"The last word is {number[^1]}"); // nove
        Console.WriteLine($"The first word is {number[^number.Length]}"); //zero
        //Console.WriteLine($"this is an exception {number[^0]}");
    }
```

### Range

E' stato introdotto l'operatore (per le liste) __".."__ che significa "prendi tutti gli indici nell'intervallo.

> 1..4 significa prendi tutti gli elementi della lista da 1 a 4 (escludi lo zero e tutto quello che viene dopo il 4)

è possibile usare questo valore senza il lato sinistro o senza il destro.

quindi:

* [6..] significa prendi gli elementi dal 6 in poi
* [..6] significa prendi tutti gli eleneti fino al 6

## Tipi di riferimento nullable

Questa caratteristica deve essere esplicitamente attivata separatamente a partire dal file di progetto

```xml
    <PropertyGroup>
    ...
    <Nullable>enable</Nullable>
    <LangVersion>8.0</LangVersion>
    </PropertyGroup>
```

> in base alla vostra versione di visual studio il tag __Nullable__ potrebbe essere modificato in __NullableReferenceTypes__.

In alternativa è possibile attivare la funzionalita in un singolo blocco di codice

* __#nullable enable__: imposta il contesto dell'annotazione nullable e il contesto dell'avviso nullable su enabled.
* __#nullable disable__: imposta il contesto dell'annotazione nullable e il contesto dell'avviso nullable su disabled.
* __#nullable safeonly__: impostare il contesto dell'annotazione nullable su enabled e il contesto dell'avviso su safeonly.
* __nullable restore__: ripristina le impostazioni di progetto per il contesto dell'annotazione nullable e il contesto dell'avviso nullable.
* __#pragma warning disable nullable__: impostare il contesto dell'avviso nullable su disabled.
* __#pragma warning enable nullable__: impostare il contesto dell'avviso nullable su enabled.
* __#pragma warning restore nullable__: ripristina le impostazioni di progetto per il contesto dell'avviso nullable.
* __#pragma warning safeonly nullable__: imposta il contesto dell'avviso nullable su safeonly.

In sostanza se la caratteristica viene attivata gli oggetti non dichiarati esplicitamente come __Nullable__, se settati a null, genereranno un warning in compilazione.

```c#
#nullable enable
    string s = null; // warning
    var txt = s.ToString(); // warning
#nullable restore
```

[dot-netcode-official-download]: https://dotnet.microsoft.com/download/dotnet-core/3.0 ".net core official download"

[vs-preview-channel]: https://visualstudio.microsoft.com/vs/preview/ "Visual studio preview chanel"

[dot-net-try]: https://github.com/dotnet/try ".net try"

[dot-net-conf]: https://www.dotnetconf.net/ ".net Conf"
[whats-new-csharp-8]: https://docs.microsoft.com/it-it/dotnet/csharp/whats-new/csharp-8 "News on C# 8"

[github-try-samples-csharp-8]: https://github.com/dotnet/try-samples/tree/master/csharp8 "Esempi di c# 8"

[extend-the-default-implementation]: https://docs.microsoft.com/it-it/dotnet/csharp/tutorials/default-interface-members-versions#extend-the-default-implementation "Default interface member"

[expression-body-definition]:https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-operator#expression-body-definition "Expression body definition"

[tutorials-pattern-matching]: https://docs.microsoft.com/it-it/dotnet/csharp/tutorials/pattern-matching "Esercitazioni sul Pattern matching"

[generate-consume-asynchronous-stream]: https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/generate-consume-asynchronous-stream
