# Novità in arrivo da 

Ci siamo quasi a fine ottobre, nella [.Net conf del 23-25 ottobre 2019][dot-net-conf] verrà rilasciata in GA la versione finale di __.net code 3__.

> Per poter vedere prima degli altri __.net core 3__ e cominciare ad apprezzare i cambiamenti potete scaricare i pacchetti che servono dalla [pagina ufficiale di .net code][dot-netcode-official-download] ppure più semplicemente passare al canale [preview di Visual studio][vs-preview-channel], _se siete utenti mac o linux di Visual  Studio probabilmente sapete già come passare al canale preview senza bisogno di maggiori informazioni_. Al momento in cui scrivo (fine agosto 2019) è stata rilasciata la __preview 8 di .net core 3__, come sempre non ci sono garanzie sul fatto che tutte le caratteristiche presenti nella preview siano poi effettivamente portate in GA ma data la vicinanza del rilascio sono ragionevolmente confidente.
>
> Vi segnalo inoltre il modulo __[dot net try][dot-net-try]__ per .net a riga di comando che apre una pagina web in cui scrivere codice in c#, utile sia per testare le novità del linguaggio sia per fare dei piccoli test senza creare decine di progetti inutili sulle nostre macchine.

Le novità, al ivello di lingiaggio che saranno introdotte dalla [.Net conf del 23-25 ottobre 2019][dot-net-conf] saranno principalmente su due fronti:

* Novità di c# versione 8
* .Net Core Versione 3

Come è ovvio le novità di .net core 3 riguarderanno "solo" l'universo creato da .net core mentre le novità introdotte nel linguaggio si rifletteranno anche (a richiesta) sul "vecchio" framework .net.

## Novità di c# 8

Potete trovare tutte le novità su [Novità di c# 8][whats-new-csharp-8] e sulla relativa [repository github][github-try-samples-csharp-8].

> Al momento in cui scrivo è stata rilasciata la versione 5 in preview di c# 8

### Membri in sola lettura

E' stato introdotto il modificatore __readonly__ su qualunque membro di una __struct__.
All'inteno di un metodo marcato come readonly non è possibile modificare il contenuto di un field perchè genererebbe un errore di compliazione. Anche il richiamare un metodo non readonly genera un warning.
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

### Modifiche alle interfaccie

Sono state introdotte varie modifiche alle interfaccie in c# 8 in particolare sono stati introdotti i metodi di default nell'interfaccia e, quasi come conseguenza naturale, la possibilità di inserire metodi statici nelle interfaccie.

> A me fa parecchio strano scrivere __definizione di metodi nell'interfaccia__

#### Membri di interfaccie predefiniti

E' Possibile "definire nell'interfaccia" dei metodi di default. _Questo rende le interfaccie un pochino più simili alle classi astratte._

> Questa nuova funzinalità è molto utile se si vuole garantire la retrocompatibilità con le versioni precedenti di una certa api.

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

E' possibile definire dei membri statici di interfaccie che varranno per tutte le classi derivate.

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

> Combinando i metodi statici definiti nelle interfaccie e le implementazioni di default dei metodi è possibile [Estendere l'impementazione predefinita][extend-the-default-implementation]

## Più pattern per l'operatore switch

Questa feature è la mia preferita perchè è presente anche in altri linguaggi moderni come swift.

> La documentazione recita _questa feature rappresenta il primo tentativo andare verso un paradigma che divida dati e funzionalità_ e noi non possiamo che esserne felici.

## switch expression

Le espressioni switch sono il naturale proseguimento dei metodi "senza corpo" o meglio delle [Expression body definition][expression-body-definition].

Questa caratteristica rende notevolmente più snello e legibile il codice di alcuni tipi di switch.

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


[dot-netcode-official-download]: https://dotnet.microsoft.com/download/dotnet-core/3.0 ".net code official download"
[vs-preview-channel]: https://visualstudio.microsoft.com/vs/preview/ "Visual studio preview chanel"
[dot-net-try]: https://github.com/dotnet/try
[dot-net-conf]: https://www.dotnetconf.net/
[whats-new-csharp-8]: https://docs.microsoft.com/it-it/dotnet/csharp/whats-new/csharp-8
[github-try-samples-csharp-8]: https://github.com/dotnet/try-samples/tree/master/csharp8
[extend-the-default-implementation]: https://docs.microsoft.com/it-it/dotnet/csharp/tutorials/default-interface-members-versions#extend-the-default-implementation
[expression-body-definition]:https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-operator#expression-body-definition
