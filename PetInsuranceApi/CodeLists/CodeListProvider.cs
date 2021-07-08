using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using PetInsuranceApi.CodeListData.Models;
using Microsoft.Extensions.Configuration;

namespace PetInsuranceApi.CodeLists
{
    public class CodeListProvider : ICodeListProvider
    {
        private const string BreedCacheKey = "BreedCache";

        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public CodeListProvider(IMemoryCache memoryCache, IConfiguration configuration)
        {
            if (memoryCache == null)
                throw new ArgumentNullException(nameof(memoryCache));

                
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public CodeList RetrieveDogs => MapBreedToCodeList("Dog", TryGetBreedCache()?.Breeds?.Dog);

        public CodeList RetrieveCats => MapBreedToCodeList("Cat", TryGetBreedCache()?.Breeds?.Cat);

        private CodeList MapBreedToCodeList(string breedName, IEnumerable<IBreed> breeds)
        {
            var items = new List<CodeItem>();
            var codeList = new CodeList
            {
                ListName = breedName,
                Items = items
            };

            if (breeds == null
                || breeds.Count() == 0)

                return codeList;

            foreach (var breed in breeds)
            {
                items.Add(new CodeItem
                {
                    FriendlyName = breed.Breed,
                    Code = breed.Group
                });
            }

            return codeList;
        }

        private BreedsCodeLists TryGetBreedCache()
        {
            BreedsCodeLists breedsCache;

            if (!_memoryCache.TryGetValue(BreedCacheKey, out breedsCache))
            {
                breedsCache = GetBreedsCodeLists();
                if (breedsCache == null)
                    throw new InvalidOperationException("Breeds code list object is null");

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30));

                _memoryCache.Set(BreedCacheKey, breedsCache, cacheEntryOptions);
            }

            return breedsCache;
        }

        private BreedsCodeLists GetBreedsCodeLists()
        {
            var breedsFilePath = _configuration["CodeListSettings:BreedsFilePath"];
            var breeds = File.ReadAllText(breedsFilePath);

            if (string.IsNullOrWhiteSpace(breeds))
                throw new InvalidOperationException("Unable to load breeds data file");

            return JsonConvert.DeserializeObject<BreedsCodeLists>(breeds);
        }
    }
}