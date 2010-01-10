COMPILE_TARGET = "debug"
require "BuildUtils.rb"

BUILD_NUMBER = "0.1.0.0"
PRODUCT = "DrivenMetrics"
COPYRIGHT = 'Released under the Apache 2.0 License';
COMMON_ASSEMBLY_INFO = 'src/CommonAssemblyInfo.cs';

versionNumber = 0.1

task :default => [:package]#:compile] #, :unit_test]

task :version do
 builder = AsmInfoBuilder.new(BUILD_NUMBER, {'Product' => PRODUCT, 'Copyright' => COPYRIGHT})
 puts "The build number is #{builder.buildnumber}"
 builder.write COMMON_ASSEMBLY_INFO  
end

task :compile => :version do
  #MSBuildRunner.compile :compilemode => COMPILE_TARGET, :solutionfile => 'src/DrivenMetrics.sln'
end

task :unit_test => :compile do
  runner = NUnitRunner.new :compilemode => COMPILE_TARGET
  runner.executeTests ['StoryTeller.Testing', 'HtmlTags.Testing']
end

task :package  do
  require 'fileutils'
  FileUtils.rm_rf 'deploy'

  Dir.mkdir 'deploy'

  FileUtils.cp 'src/DrivenMetric.UI.Console/bin/Release/DrivenMetric.UI.Console.exe', 'deploy'
  FileUtils.cp 'src/DrivenMetric.UI.Console/bin/Release/DrivenMetrics.dll', 'deploy'
  FileUtils.cp 'src/DrivenMetric.UI.Console/bin/Release/Mono.Cecil.Extensions.dll', 'deploy'
end



