Welcome to the DrivenMetrics Project
========================================

DrivenMetrics is a metrics library to be used with your .net project. It is used to help with determining the quality of your code by measuring certain attributes or metrics.

How to Build DrivenMetrics
--------------------------
To build project, rake is required. 

For .net
	rake
	
For mono
	rake mono

The final build will be in a folder called *deploy*

How to run DrivenMetrics
--------------------------

run the following for help
    DrivenMetric.UI.Console.exe /help 

And example would be
    DrivenMetric.UI.Console.exe -a "an assembly" -a "another assembly" -cc=20 -loc=20  -rFail

TODO
----

* Look at adding a dynamic way to add reports and metrics
