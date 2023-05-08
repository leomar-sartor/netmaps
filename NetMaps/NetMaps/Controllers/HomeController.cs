using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using NetMaps.Interfaces;
using NetMaps.Models;
using System.Diagnostics;
using System.Xml.Linq;

namespace NetMaps.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly GeoContext _context;
        private readonly IGasStationAppService _gasStationService;

        public HomeController(ILogger<HomeController> logger
            //,IOptions<SettingsConfig> settings
            , IGasStationAppService gasStationService
            )
        {
            _logger = logger;
            //_context = new GeoContext(settings);
            _gasStationService = gasStationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            //var meusPontos = _context.Pontos;

            //Contexto
            ContextoMongoDb dbContext = new ContextoMongoDb();
            
            //Create
            var newUser = new Usuario() { Id = Guid.NewGuid(), Nome = "Primeiro" };
            await dbContext.Usuario.InsertOneAsync(newUser);

            //Search
            Guid id = newUser.Id;
            var user = await dbContext.Usuario.Find(u => u.Id == id).FirstOrDefaultAsync();

            //Update
            newUser.Nome = "Primeiro Atualizado";
            await dbContext.Usuario.ReplaceOneAsync(u => u.Id == id, newUser);

            //Search Novamente
            var userDeNovo = await dbContext.Usuario.Find(u => u.Id == id).FirstOrDefaultAsync();

            //List
            var resList = await dbContext.Usuario.Find(u => true).ToListAsync();

            //Delete
            await dbContext.Usuario.DeleteOneAsync(u => u.Id == id);

            return await Task.Run(() => View());

        }

        //https://medium.com/xp-inc/mongodb-utilizando-dados-geogr%C3%A1ficos-b0e07a31211d
        //https://dev.to/djnitehawk/tutorial-geospatial-search-in-mongodb-the-easy-way-kbd
        //https://devforid.medium.com/mongodb-indexing-using-c-driver-d66e3b314e72
        //https://www.mongodb.com/developer/languages/csharp/mongodb-geospatial-queries-csharp/
        //https://www.mongodb.com/docs/manual/core/2dsphere/
        public async Task<IActionResult> Distancia()
        {
            //Inserindo um Posto de Combustivel
            var input = new GasStationInput()
            {
                Name = "Posto Leomar",
                Email = "postoleomar@uno.com.br",
                Location = new Location()
                {
                    Type = "Point",
                    Coordinates = new List<double>() { -23.2118781, -46.8662337 }
                }
            };

            var obj = await _gasStationService.AddAsync(input);



            //Buscar postos próximos de onde estou
            var proximos = await _gasStationService.GetProximityAsync(-23.2093854, -46.8694894, "");
            //Veja, que no objeto de retorno dist, existe a propriedade calculated, onde me diz, a distância do ponto onde estou, para o posto de gasolina, que neste exemplo é de 409,07 metros.





            return await Task.Run(() => View());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}