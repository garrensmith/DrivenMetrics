COMPILE_TARGET = "Release"
require "./BuildUtils"
require "albacore"

task :default => [:net]
task :mono => [:assemblyinfo, :compile_mono, :mono_unit_test, :zip]
task :net => [:assemblyinfo, :compile_net, :unit_test, :package]


desc "Run a sample assembly info generator"
assemblyinfo :assemblyinfo do |asm|
  asm.version = "0.1.0.0"
  asm.company_name = "Garren Smith"
  asm.product_name = "Driven Metrics"
  asm.title = "Driven Metrics"
  asm.description = "Driven Metrics, Open Source Code analyzer"
  asm.copyright = "copyright 2011, by Garren Smith"
  asm.output_file = "src/CommonAssemblyInfo.cs"
end


task :compile_net => :version do
  MSBuildRunner.compile :compilemode => COMPILE_TARGET, :solutionfile => 'src/DrivenMetrics.sln'
end

desc "Run a sample build using the MSBuildTask"
xbuild :xbuild do |msb|
  msb.properties :configuration => :Debug
  msb.targets :Clean, :Build
  msb.solution = "src/DrivenMetrics.sln"
end


task :compile_mono => :assemblyinfo do
  XBuildRunner.compile :solutionfile => 'src/DrivenMetrics.sln'
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
  FileUtils.cp 'lib/Mono/Mono.Cecil.Mdb.dll', 'deploy'
  puts ""
  puts "DONE!"
end

zip :zip => :package do |zip|
     zip.directories_to_zip "deploy"
     zip.output_file = 'DrivenMetrics.zip'
     zip.output_path = File.dirname(__FILE__)
end




