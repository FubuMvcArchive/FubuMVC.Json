require 'fuburake'


FubuRake::Solution.new do |sln|
	sln.compile = {
		:solutionfile => 'src/FubuMVC.Json.sln'
	}
				 
	sln.assembly_info = {
		:product_name => "FubuMVC.Json",
		:copyright => 'Copyright 2012-2013 Josh Arnold, Jeremy D. Miller, et al. All rights reserved.'
	}
	
	sln.ripple_enabled = true
	sln.fubudocs_enabled = true
	


	sln.options[:nuget_publish_folder] = 'nupkgs'
	sln.options[:nuget_publish_url] = 'https://www.myget.org/F/fubumvc-edge/'


end
