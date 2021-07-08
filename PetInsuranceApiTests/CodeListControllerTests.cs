using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetInsuranceApi;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using System;
using PetInsuranceApi.CodeLists;
using PetInsuranceApi.Controllers;
using Microsoft.Extensions.Logging;
using System.IO;
using PetInsuranceApi.CodeListData.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace PetInsuranceApiTests
{
    [TestClass]
    public class CodeListControllerTests
    {
        private string breedsDataFile = "../../../../PetInsuranceApi/CodeListData/breeds.json";
        private BreedsCodeLists breeds;
        
        private CodeListController codeListController;

        [TestInitialize]
        public void Initialize()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.SetupGet<string>(x => x["CodeListSettings:BreedsFilePath"]).Returns(breedsDataFile);

            var _memoryCache = new MemoryCache(new MemoryCacheOptions());

            var loggerMock = new Mock<ILogger<CodeListController>>();

            var codeListProvider = new CodeListProvider(_memoryCache, configMock.Object);

            codeListController = new CodeListController(loggerMock.Object, codeListProvider); 

            var breedsFile = File.ReadAllText(breedsDataFile);
            breeds = JsonConvert.DeserializeObject<BreedsCodeLists>(breedsFile);
        }

        [TestMethod]
        public void GetDogs_ValidDataSet_AllDogsPresent()
        {
            var actionResult = codeListController.GetDogs() as OkObjectResult;
            Assert.AreEqual(200, actionResult.StatusCode);

            var dogsList = actionResult.Value as CodeList;
            Assert.IsNotNull(dogsList);
            Assert.AreEqual("Dog", dogsList.ListName);

            var dogs = dogsList.Items.ToList();

            foreach(var d in breeds.Breeds.Dog)
            {
                Assert.IsTrue(dogs.Exists(x => x.FriendlyName == d.Breed && x.Code == d.Group));
            }
        }

        [TestMethod]
        public void GetCats_ValidDataSet_AllDogsPresent()
        {
            var actionResult = codeListController.GetCats() as OkObjectResult;
            Assert.AreEqual(200, actionResult.StatusCode);

            var catList = actionResult.Value as CodeList;
            Assert.IsNotNull(catList);
            Assert.AreEqual("Cat", catList.ListName);

            var cats = catList.Items.ToList();

            foreach(var c in breeds.Breeds.Cat)
            {
                Assert.IsTrue(cats.Exists(x => x.FriendlyName == c.Breed && x.Code == c.Group));
            }
        }

        [TestMethod]
        public void GetDogs_EmptyDataSet_ReturnsEmptyList()
        {
            breedsDataFile = "../../../breedsEmpty.json";

            Initialize();

            var actionResult = codeListController.GetDogs() as OkObjectResult;
            Assert.AreEqual(200, actionResult.StatusCode);

            var dogList = actionResult.Value as CodeList;
            Assert.IsNotNull(dogList);
            Assert.AreEqual("Dog", dogList.ListName);
            Assert.AreEqual(0, dogList.Items.Count());
        }

        [TestMethod]
        public void GetCats_EmptyDataSet_ReturnsEmptyList()
        {
            breedsDataFile = "../../../breedsEmpty.json";

            Initialize();

            var actionResult = codeListController.GetCats() as OkObjectResult;
            Assert.AreEqual(200, actionResult.StatusCode);

            var catList = actionResult.Value as CodeList;
            Assert.IsNotNull(catList);
            Assert.AreEqual("Cat", catList.ListName);
            Assert.AreEqual(0, catList.Items.Count());
        }

        [TestMethod]
        public void GetDogs_Exception_Returns500()
        {
            breedsDataFile = "../../../breedsInvalid.json";

            Initialize();

            var actionResult = codeListController.GetDogs() as StatusCodeResult;
            Assert.AreEqual(500, actionResult.StatusCode);
        }

        [TestMethod]
        public void GetCats_Exception_Returns500()
        {
            breedsDataFile = "../../../breedsInvalid.json";

            Initialize();

            var actionResult = codeListController.GetCats() as StatusCodeResult;
            Assert.AreEqual(500, actionResult.StatusCode);
        }
    }
}
