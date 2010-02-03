Welcome to the DrivenMetrics Project
========================================

DrivenMetrics is a metrics library to be used with your .net project. It is used to help with determining the quality of your code by measuring certain attributes or metrics.

How to Build DrivenMetrics
--------------------------
To build in on windows have ruby and rake installed and run rake in the base directory. The final build will be in a folder called *deploy*

How to run DrivenMetrics
--------------------------

run the following for help
    DrivenMetric.UI.Console.exe /help 

And example would be
    DrivenMetric.UI.Console.exe -a "an assembly" -a "another assembly" -cc=20 -loc=20  -rFail

TODO
----

Get it tests passing on mono
Implement build on mono with xbuild
Add Rake and Nant useablity
Look at adding a dynamic way to add reports and metrics
