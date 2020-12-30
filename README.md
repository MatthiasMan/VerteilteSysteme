ein example wo das ganze mit 3 workern gestartet wird, ist im example.mp4 zu sehen

Um zu starten müssen die Projekte:

MandlBrot      (=der wpf client, nicht schrecken, er beginnt erst etwas darzustellen, wenn alle daten da sind)
Ventilator     (=der verteiler der aufgaben)
Worker         (kann beliebig oft gestartet werden, =der arbeiter, der einen task erledigt)

gestartet werden (reihenfolge egal) (es kann die .exe jeweils z.b. /MandlBrot/bin/Debug/netcoreapp3.1/MandlBrot.exe oder über Visual Studio die Projekte gestartet werden)

nun muss beim ventilator 1-2 mal ENTER gedrückt werden (wenn es sich schließt, hat man zu oft (oder zu schnell) gedrückt, falls nix arbeitet, muss man nochmal ENTER drücken)

nun sollte der ventilator die aufgaben verteilen und die worker ihre tasks abarbeiten

wenn alles abgearbeitet ist, öffnet sich das wpf fenster vom MandlBrot und zeichnet das MandlBrot (kann etwas dauern)

bei änderung oder erneutem starten wollen, muss der gesamte Vorgang wiederholt werden.

Troubleshooting:
Falls socket/port probleme auftreten, darauf achten, dass vorallem 
Port 80 (der wird vo vielen andren applikationen a verwendet), und 400 offen sind

Wenn die einstellungen von height geändert werden will, muss unter
Ventilator => Program => int height = 200; umgestellt werden UND 
MandlBrot => MainWindow.xaml.cs => for (int i = 0; i < 40; i++)  ......hier muss für (height200=> i<40) (height400=> i<80) (height300=> i<60) also immer die height durch 5 genommen werden (=2mal die anzahl an packeten)

für die width muss
Ventilator => Program => int width = 200; umgestellt werden

PS.: Guten Rutsch ins neue Jahr! bzw (falls danach angeschaut) hoffe ich, du bist gut rübergerutscht ins neue Jahr! :)