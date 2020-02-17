# NDepend

Qualche settimana fa sono stato contattato da Peter Smacchia su  Linkedin per provare il loro software per scrivere poi delle impressioni avute.
Non avendo prima idea precsa di cosa facesse il software mi sono prima documentato in rete per avere un'idea pre


## Partiamo dalle basi. Cosa è Ndepend.

NDepend è un software per l'analisi statica del codice che vi permette di tenere monitorate delle metriche, interagisce con Visual Studio e con Azure Devops e fornisce dei report sula "qualità" del codice che scrivete.

Appena lo aprirete vi triverete davanti ad un software **molto complesso** ma è giusto così. _NDepend_ non è un software pensato per essere un punta e clicca ma un software per _architetti_,  _tech lead_ o programmatori con una certa esperienza, ma non voglio ora anticipare le conclusioni.

Appena installato NDepend e appena fatto girare su una solution esistente colpisce per la completezza e la complessità dei dati proposti.
So strumeno principale per avere una rapida overview del codice della vostra soluzione.
I concetti chiave nella dashboard (e nel software in generale) sono tre (le altre cose si "capiscono" abbastanza agevolmente).

AL contrario di come ho fatto io vi consigli di installare la versione "stand alone" di NDepend e l'extension per visualstudio che è (a mio parere) un pochino pià agevole nell'utilizzo.

Una volta avviato ndepend (sia come estenzione di VS che come sprogramma autonomo) non vi resta altro da fare che analizzare una soluzione Visual Studio o aprire un progetto Ndepend nuovo. Nel caso in cui usiate Ndepend come estenzione di visual studio potrete anche agganciare un progetto ndepend alla vostra soluzione (cosa che faccio abitualmente perchè lavorando in un progetto condiviso con tanti utenti non salvo il progetto di ndepend nella repo con tutti gli altri ed ogni volta che mi sposto di branch "perdo" l'associazione tra progetto visual studio e ndepend).

Sembra banale a dirsi ma "salvare il progetto" di ndepend anche se ad occhio dovrete far girare molte volte l'analisi per apprezzare le modifiche fatte alla vostra codebase non serve solo a "salvare" le impostazioni personali che avete dato al progetto, serve anche (sopratutto) a tenere traccia dei vari indicatori nel tempo e a fare delle analisi di trend sul vostro codice.

In generale dopo aver fatto partire NDepend vi troverete davanti ad una dashboard ce riassume tutti i paraetri analizzati styaticamente dal vostro codice.

In generale nella dashboard di NDepend sono presneti le 

* [Code Metrics][ndepend-codemetrics]
* [Quality Gate][ndepend-qualitygate]
* Issue
* Rules

L'insieme di questi quattro "elementi" ci consente di fare una stima molto accurata della qualità del nostro codice.
In generale è possibile stabilire delle regole "di scrittura del codice" che volete adottare nella vostra soluzione, ad esempio "Avoid having different types with same name", questa regola genererà delle issue nel codice (nel caso in cui non vosse rispettata).

E' possibile inoltre stabilire dei quality gate che ci diranno se, in base alle "regole" che ci siamo dati, possiamo o meno andare in produzione. questi quality gate si basano su issue e su code metrics (code coverage, complessità ciclomatica etc etc)

Con uno screenshot è tutto molto più chiaro.

![NDepend Main Dashboard][ndepend-main-dashboard]

Particolarmente interessanti sono i quality gate che analizzano i cambiamenti nel tempo; mi spiego meglio.
Ci sono dei quality gate che non consentono di andare in produzione se non si ottiene una certa percentuale di copertura di unit test sul nuovo codice o se non si è scritta la documentazione al nuovo codice scritto.
Questo perchè sappiamo bene che non sempre il codice su cui lavoriamo è fatto a regola d'arte. NDepend ci aiuta sia a "mettere in bolla" il codice già scritto che a non lasciarsi andare alla "poetica del codice già scritto".

Alla fine dell'analisi NDepend vi fornisce anche il debito tecnico che è praticamente il Kpi dei Kpi. in pratica è quanto vi serve (in tempo) a risolvere tutte le issue presenti sul codice. 
Ovviamente è un numero da prendere con le molle e solo l'esperienza del vostro team vi puo' portare ad una stima corretta del debito tecnico pero' almeno fornisce un inizio.

## Una quantità infinita di grafi

Ci sono una quantità di grafi che è impossibile da riassumere.
Diciamo che più o meno qualunque sia la metrica del vostro codice che vogliate tenere d'occhio con un grafo la troverete in NDepend.
Io ho trovato particolarmente utile (per il mio progetto) il grafo della code coverage perchè mi ha consentito di tenere sotto'cchio la copertura del codice, la grandezza e la grandezza dei metodi (in blocchi) e mi ha consentito di rimettere in bolla (o quasi) il progetto sterminato che avevo ereditato.

![NDepend Code Coverage][ndepend-code-coverage]

## NDepend come un PRO!

Come se come strumento non fosse giù abbastanza pro ndepend consente di essere customizzato!
Come accade anche utilizzando strumenti molto più semplici come StyleCop (che fanno una frazione di quello che fa NDepend) è possibile scrivere delle query con un linguaggio specifico che si chiama [CQLinq][ndepend-cqsintax] che, come dice il nome, è un linguaggio basato su linq.

```
// <Name>Avoid empty interfaces</Name>
// <Id>ND1212:AvoidEmptyInterfaces</Id>
warnif count > 0 from t in JustMyCode.Types where 
   t.IsInterface && 
   t.NbMethods == 0 &&
  !t.InterfacesImplemented.Any()
select new { 
   t, 
   t.TypesThatImplementMe,
   Debt = (10 + 3*t.TypesThatImplementMe.Count()).ToMinutes().ToDebt(),
   Severity = t.TypesThatImplementMe.Any() ? Severity.Medium : Severity.Low
}
```

Scrivere una regola non è una cosa affatto banale. Personalmente io all'inizio mi "accontanto" di affidarmi alle Rule ed ai quality gate (anche quelli sono esprimibili tramite query) Out Of The Box.

Ma un vero pro non puo' fare a meno della riga di comando!
Ebbene Ndepend ha sia una console che dei powertools (di cui ovviamente esiste anche il sorgente)

![Powertool][ndepend-powertool]

> Probabilemnte la dashboard non era abbastanza nerd

## Conclusioni e prezzi

Come avrete capito NDepend è un software letteralmente sterminato. Una cosa fatta da programmatori per programamatori.
Non è sempre facile ed intuitivo, ha una curva di apprendimento abbastanza ripida, ma non è un software per tutti.

Se dovete gestire molto codice e fare una review costante della vostra code base NDepend è il software che fa per voi.
Se non siete architetti o teamleader o siete programmatori alle prime armi tenetevi alla larga non farà altro che conforndervi le idee.

Certo è che uno strumenti di analisi statica del codice puo' portarvi un livello oltre.

In generale NDepend riassume in se (e per certi versi semplifica) l'accesso ad una serie di informazioni e metriche del vostro codice a cui potevate accedere con decine di altri "entry point". Viene da se che essendo uno strumento cosi' grande e potente deve essere utilizzato con "ragionevolezza"; ad esempio se voi usate Stylecop per tenere sott'occhio lo stile con cui scrivete il codice e su NDepend segnate una regola in contrasto con le regole che avete deciso di seguire su NDepend rischiate di entrare in un circolo visioso. 

Le considerazioni sulla natura PRO del prodotto sono anche dovute dal fattore [prezzo][ndepend-purchase] che ovviamente non sono economici o cumunque alla portata di un programmatore singolo che voglia usarlo "per sfizio" per tenere sotto controllo il proprio codice.


[ndepend-codemetrics]: https://www.ndepend.com/docs/code-metrics
[ndepend-qualitygate]: https://www.ndepend.com/docs/quality-gates  
[ndepend-purchase]: https://www.ndepend.com/purchase
[ndepend-cqsintax]:https://www.ndepend.com/docs/cqlinq-syntax

[ndepend-main-dashboard]: dashboard.png
[ndepend-code-coverage]: MetricTreemapSnapshot0.png
[ndepend-powertool]: powertool.png