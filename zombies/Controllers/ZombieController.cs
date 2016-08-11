using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SampleMicroservice.Controllers
{
	[Route("api/zombies")]
	public class ZombieController
	{
		private IZombieRepository repository;
		private IGlobalCounter counter;
		private ZombieOptions options;
		private readonly ILogger<ZombieController> _logger;



		public ZombieController(IZombieRepository zombieRepository,
								IGlobalCounter globalCounter,
								IOptions<ZombieOptions> zombieOptionsAccessor,
								ILogger<ZombieController> logger) {
			repository = zombieRepository;
			counter = globalCounter;
			options = zombieOptionsAccessor.Value;
			_logger = logger;
		}

		[HttpGet]
    	public IEnumerable<Zombie> Get()
    	{	
				counter.Increment();
				Console.WriteLine("count:" + counter.Count);
				Console.WriteLine("options:\n" +
					"\tAdvanced zombie tracking: " + options.EnableAdvancedZombieTracking +
					", zombie name: '" + options.ZombiesDescription + "', tag count: " + options.ZombieTags.Count);
					_logger.LogDebug("Zombie get");
        	return repository.ListAll();
    	}
	}
}
