COMPILE_TARGET = "Release"
require "BuildUtils.rb"

BUILD_NUMBER = "0.1.0.0"
PRODUCT = "DrivenMetrics"
COPYRIGHT = 'Released under the Apache 2.0 License';
COMMON_ASSEMBLY_INFO = 'src/CommonAssemblyInfo.cs';

versionNumber = 0.1

task :default => [:net]
task :mono => [:version, :compile_mono,:mono_unit_test, :package]
task :net => [:version, :compile_net, :unit_test, :package]

task :version do
 builder = AsmInfoBuilder.new(BUILD_NUMBER, {'Product' => PRODUCT, 'Copyright' => COPYRIGHT})
 puts "The build number is #{builder.buildnumber}"
 builder.write COMMON_ASSEMBLY_INFO  
end

task :compile_net => :version do
  MSBuildRunner.compile :compilemode => COMPILE_TARGET, :solutionfile => 'src/DrivenMetrics.sln'
end

task :compile_mono => :version do
  XBuildRunner.compile :compilemode => COMPILE_TARGET, :solutionfile => 'src/DrivenMetrics.sln'
end

task :unit_test  do
  runner = NUnitRunner.new :compilemode => COMPILE_TARGET
  runner.executeTests ['DrivenMetrics.Tests']
end

task :mono_unit_test  do
  runner = MonoNUnitRunner.new :compilemode => COMPILE_TARGET
  runner.executeTests ['DrivenMetrics.Tests']
end

task :package  do
  puts "Packaging into deploy"
  
  require 'fileutils'
  FileUtils.rm_rf 'deploy'

  Dir.mkdir 'deploy'

  FileUtils.cp 'src/DrivenMetric.UI.Console/bin/Release/DrivenMetric.UI.Console.exe', 'deploy'
  FileUtils.cp 'src/DrivenMetric.UI.Console/bin/Release/DrivenMetrics.dll', 'deploy'
  FileUtils.cp 'src/DrivenMetric.UI.Console/bin/Release/Mono.Cecil.Extensions.dll', 'deploy'
  FileUtils.cp 'src/DrivenMetric.UI.Console/bin/Release/Mono.Cecil.dll', 'deploy'
  FileUtils.cp 'lib/Mono/Mono.Cecil.Pdb.dll', 'deploy'
  puts ""
  puts "DONE!"
end



