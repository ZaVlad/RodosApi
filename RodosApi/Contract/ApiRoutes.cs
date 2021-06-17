using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract
{
    public class ApiRoutes
    {
        private const string Version = "v1";
        private const string Api = "api";
        private const string Base = Api + "/" + Version;

        public class TypeOfDoors
        {
            public const string GetTypes = Base + "/types";

            public const string GetTypeOfDoor = Base + "/types/{typeId}";

            public const string CreateType = Base + "/types";

            public const string UpdateType = Base + "/types{typeId}";

            public const string DeleteType = Base + "/types{typeId}";
        }

        public class Coatings
        {
            public const string GetCoatings = Base + "/coatings";

            public const string GetCoating = Base + "/coatings/{coatingId}";

            public const string CreateCoating = Base + "/coatings";

            public const string UpdateCoating = Base + "/coatings/{coatingId}";

            public const string DeleteCoating = Base + "/coatings/{coatingId}";
        }

        public class Collection
        {
            public const string GetCollections = Base + "/collections";

            public const string GetCollection = Base + "/collections/{collectionId}";

            public const string CreateCollections = Base + "/collections";

            public const string UpdateCollections = Base + "/collections/{collectionId}";

            public const string DeleteCollections = Base + "/collections/{collectionId}";

        }

        public class DoorModel
        {
            public const string GetDoorModels = Base + "/doorModels";
            public const string GetDoorModel = Base + "/doorModels/{doorModelId}";
            public const string CreateDoorModel = Base + "/doorModels";
            public const string UpdateDoorModel = Base + "/doorModels/{doorModelId}";
            public const string DeleteDoorModel = Base + "/doorModels/{doorModelId}";
        }

        public class Colors
        {
            public const string GetColors = Base + "/colors";
            public const string GetColor = Base + "/colors/{colorId}";
            public const string CreateColor= Base + "/colors";
            public const string UpdateColor= Base + "/colors/{colorId}";
            public const string DeleteColor= Base + "/colors/{colorId}";
        }

        public class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
        }
        public class Countries
        {
            public const string GetCountries = Base + "/countries";
            public const string GetCountry = Base + "/countries/{countryId}";
            public const string CreateCountry = Base + "/countries";
            public const string UpdateCountry = Base + "/countries/{countryId}";
            public const string DeleteCountry = Base + "/countries/{countryId}";
        }

        public class Category
        {
            public const string GetCategories = Base + "/categories";
            public const string GetCategory= Base + "/categories/{categoryId}";
            public const string CreateCategory = Base + "/categories";
            public const string UpdateCategory = Base + "/categories/{categoryId}";
            public const string DeleteCategory = Base + "/categories/{categoryId}";
        }

        public class Maker
        {
            public const string GetMakers = Base + "/makers";
            public const string GetMaker = Base + "/makers/{makerId}";
            public const string CreateMaker = Base + "/makers";
            public const string UpdateMaker = Base + "/makers/{makerId}";
            public const string DeleteMaker = Base + "/makers/{makerId}";
        }

        public class FurnitureType
        {
            public const string GetFurnitureTypes = Base + "/furnitureType";
            public const string GetFurnitureType = Base + "/furnitureType/{furnitureTypeId}";
            public const string CreateFurnitureType = Base + "/furnitureType";
            public const string UpdateFurnitureType = Base + "/furnitureType/{furnitureTypeId}";
            public const string DeleteFurnitureType = Base + "/furnitureType/{furnitureTypeId}";
        }

        public class Material
        {
            public const string GetMaterials = Base + "/materials";
            public const string GetMaterial = Base + "/materials/{materialId}";
            public const string CreateMaterial = Base + "/materials";
            public const string UpdateMaterial = Base + "/materials/{materialId}";
            public const string DeleteMaterial = Base + "/materials/{materialId}";
        }

        public class TypeOfHinges
        {
            public const string GetTypesOfHinges = Base + "/typeOfHinges";
            public const string GetTypeOfHinges = Base + "/typeOfHinges/{typeOfHingesId}";
            public const string CreateTypeOfHinges = Base + "/typeOfHinges";
            public const string UpdateTypeOfHinges = Base + "/typeOfHinges/{typeOfHingesId}";
            public const string DeleteTypeOfHinges = Base + "/typeOfHinges/{typeOfHingesId}";
        }
        public class Hinges
        {
            public const string GetHinges = Base + "/hinges";
            public const string GetHinge = Base + "/hinges/{hingesId}";
            public const string CreateHinge = Base + "/hinges";
            public const string UpdateHinge = Base + "/hinges/{hingesId}";
            public const string DeleteHinge = Base + "/hinges/{hingesId}";
        }
        public class DoorHandle
        {
            public const string GetDoorHandles = Base + "/doorHandles";
            public const string GetDoorHandle = Base + "/doorHandles/{doorHandleId}";
            public const string CreateDoorHandle = Base + "/doorHandles";
            public const string UpdateDoorHandle = Base + "/doorHandles/{doorHandleId}";
            public const string DeleteDoorHandle = Base + "/doorHandles/{doorHandleId}";
        }

        public class Door
        {
            public const string GetDoors = Base + "/doors";
            public const string GetDoor= Base + "/doors/{doorId}";
            public const string CreateDoor= Base + "/doors";
            public const string CreateDoors = Base + "/createDoors";
            public const string UpdateDoor= Base + "/doors/{doorId}";
            public const string DeleteDoor= Base + "/doors/{doorId}";
        }
        public class Client
        {
            public const string GetClients = Base + "/clients";
            public const string GetClient = Base + "/clients/{clientId}";
            public const string CreateClient = Base + "/clients";
            public const string UpdateClient = Base + "/clients/{clientId}";
            public const string DeleteClient = Base + "/clients/{clientId}";
        }
        public class Order
        {
            public const string GetOrders = Base + "/orders";
            public const string GetOrder = Base + "/orders/{orderId}";
            public const string CreateOrder = Base + "/orders";
            public const string UpdateOrder = Base + "/orders/{orderId}";
            public const string DeleteOrder = Base + "/orders/{orderId}";
        }

    }
}
