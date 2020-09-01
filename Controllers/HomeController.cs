using AutoMapper;
using SmartFarming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartFarming.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            homeModel model = new homeModel();
            var cropDetails = (from c in db.Crops
                               join ct in db.CropTypes on c.type_id equals ct.id
                               select new CropDetails
                               {
                                   id = c.id,
                                   CropName = c.CropName,
                                   CropImage = c.CropImage,
                                   CropType = ct.Croptype1
                               }).ToList();
            var newsList = (from c in db.News
                            select new NewsView
                            {
                                id = c.id,
                                Headline = c.Headline,
                                Text = c.Headline,
                                Link = c.Link,
                                Date = c.Date
                            }).ToList();
            model.Crops = cropDetails;
            model.News = newsList;
            return View(model);
        }

        [HttpGet]
        public ActionResult Crops()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            List<CropDetails> foodCrops = (from c in db.Crops
                                           where c.type_id == 1
                                           select new CropDetails
                                           {
                                               id = c.id,
                                               CropName = c.CropName,
                                               CropImage = c.CropImage,
                                               Kharif = (bool)c.Kharif,
                                               Rabi = (bool)c.Rabi,
                                               Zaid = (bool)c.Zaid
                                           }).ToList();
            foreach (var item in foodCrops)
            {
                List<string> season = new List<string>();
                if (item.Kharif == true)
                {
                    season.Add("Kharif");
                }
                if (item.Rabi == true)
                {
                    season.Add("Rabi");
                }
                if (item.Zaid == true)
                {
                    season.Add("Zaid");
                }
                item.Season = string.Join(", ", season);
            }

            List<CropDetails> cashCrops = (from c in db.Crops
                                           where c.type_id == 2
                                           select new CropDetails
                                           {
                                               id = c.id,
                                               CropName = c.CropName,
                                               CropImage = c.CropImage,
                                               Kharif = (bool)c.Kharif,
                                               Rabi = (bool)c.Rabi,
                                               Zaid = (bool)c.Zaid
                                           }).ToList();
            foreach (var item in cashCrops)
            {
                List<string> season = new List<string>();
                if (item.Kharif == true)
                {
                    season.Add("Kharif");
                }
                if (item.Rabi == true)
                {
                    season.Add("Rabi");
                }
                if (item.Zaid == true)
                {
                    season.Add("Zaid");
                }
                item.Season = string.Join(", ", season);
            }

            List<CropDetails> plantationCrops = (from c in db.Crops
                                                 where c.type_id == 3
                                                 select new CropDetails
                                                 {
                                                     id = c.id,
                                                     CropName = c.CropName,
                                                     CropImage = c.CropImage,
                                                     Kharif = (bool)c.Kharif,
                                                     Rabi = (bool)c.Rabi,
                                                     Zaid = (bool)c.Zaid
                                                 }).ToList();
            foreach (var item in plantationCrops)
            {
                List<string> season = new List<string>();
                if (item.Kharif == true)
                {
                    season.Add("Kharif");
                }
                if (item.Rabi == true)
                {
                    season.Add("Rabi");
                }
                if (item.Zaid == true)
                {
                    season.Add("Zaid");
                }
                item.Season = string.Join(", ", season);
            }

            List<CropDetails> horticultureCrops = (from c in db.Crops
                                                   where c.type_id == 4
                                                   select new CropDetails
                                                   {
                                                       id = c.id,
                                                       CropName = c.CropName,
                                                       CropImage = c.CropImage,
                                                       Kharif = (bool)c.Kharif,
                                                       Rabi = (bool)c.Rabi,
                                                       Zaid = (bool)c.Zaid
                                                   }).ToList();
            foreach (var item in horticultureCrops)
            {
                List<string> season = new List<string>();
                if (item.Kharif == true)
                {
                    season.Add("Kharif");
                }
                if (item.Rabi == true)
                {
                    season.Add("Rabi");
                }
                if (item.Zaid == true)
                {
                    season.Add("Zaid");
                }
                item.Season = string.Join(", ", season);
            }

            CropsViewModel model = new CropsViewModel();

            model.FoodCrops = foodCrops;
            model.CashCrops = cashCrops;
            model.PlantationCrops = plantationCrops;
            model.HorticultureCrops = horticultureCrops;

            return View(model);
        }

        [HttpGet]
        public ActionResult CropDetail(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var cropDetail = (from c in db.Crops
                              join ct in db.CropTypes on c.type_id equals ct.id
                              join hp in db.States on c.HighestProducers equals hp.id
                              where c.id == id
                              select new CropDetails
                              {
                                  id = c.id,
                                  CropName = c.CropName,
                                  CropImage = c.CropImage,
                                  CropType = ct.Croptype1,
                                  Kharif = (bool)c.Kharif,
                                  Rabi = (bool)c.Rabi,
                                  Zaid = (bool)c.Zaid,
                                  CropDesc = c.CropDesc,
                                  LandPreparation = c.LandPreparation,
                                  SowingMethod = c.SowingMethod,
                                  Harvesting = c.Harvesting,
                                  Temp = c.Temp,
                                  RainFall = c.RainFall,
                                  SoilType = c.SoilType,
                                  HighestProducers = hp.State1,
                                  MajorProducers = (from mp in db.MajorProducers
                                                    join s in db.States on mp.state_id equals s.id
                                                    where mp.crop_id == id
                                                    select s.State1.ToString()).ToList()
                              }).FirstOrDefault();


            List<string> season = new List<string>();
            if (cropDetail.Kharif == true)
            {
                season.Add("Kharif");
            }
            if (cropDetail.Rabi == true)
            {
                season.Add("Rabi");
            }
            if (cropDetail.Zaid == true)
            {
                season.Add("Zaid");
            }
            cropDetail.Season = string.Join(", ", season);

            var seedList = (from m in db.Seeds
                            where m.crop_id == id
                            select new ManageSeeds()
                            {
                                id = m.id,
                                SeedName = m.SeedName,
                                Image = db.SeedImages.Where(x => x.seed_id == m.id).FirstOrDefault().Image.ToString(),
                                Price = (decimal)m.Price
                            }).ToList();

            var pesticideList = (from m in db.Pesticides
                                 where m.crop_id == id
                                 select new ManagePesticides()
                                 {
                                     id = m.id,
                                     Image = m.PesticideImage,
                                     PesticideName = m.PesticideName,
                                     Category = m.Category,
                                     Price = (decimal)m.Price
                                 }).ToList();

            var fertilizerList = (from m in db.Fertilizers
                                  where m.crop_id == id
                                  select new ManageFertilizers()
                                  {
                                      id = m.id,
                                      Image = m.FertilizerImage,
                                      FertilizerName = m.FertilizerName,
                                      Material = m.Material,
                                      Price = (decimal)m.Price
                                  }).ToList();
            CropById cropModel = new CropById()
            {
                cropDetail = cropDetail,
                pesticideList = pesticideList,
                seedList = seedList,
                fertilizerList = fertilizerList
            };
            return View(cropModel);
        }

        [HttpGet]
        public ActionResult Seeds()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            var seedList = (from m in db.Seeds
                            select new ManageSeeds()
                            {
                                id = m.id,
                                SeedName = m.SeedName,
                                Image = db.SeedImages.Where(x => x.seed_id == m.id).FirstOrDefault().Image,
                                Price = (decimal)m.Price
                            }).ToList();
            return View(seedList);
        }

        [HttpGet]
        public ActionResult SeedDetail(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var SeedImagesList = db.SeedImages.Where(m => m.seed_id == id).Select(m => m.Image.ToString()).ToList();
            var seedDetails = (from s in db.Seeds
                               join c in db.Crops on s.crop_id equals c.id
                               join u in db.Users on s.dealer_id equals u.id
                               join ci in db.Cities on u.city_id equals ci.id
                               join si in db.States on u.state_id equals si.id
                               where s.id == id
                               select new SeedDetails()
                               {
                                   id = s.id,
                                   crop_id = s.crop_id,
                                   CropName = c.CropName,
                                   dealer_id = u.id,
                                   DealerName = u.Name,
                                   Address = u.Address,
                                   City = ci.City1,
                                   State = si.State1,
                                   Email = u.Email,
                                   ContactNo = u.ContactNo,
                                   SeedName = s.SeedName,
                                   Season = s.Season,
                                   PestTolerance = s.PestTolerance,
                                   Price = (decimal)s.Price,
                                   Duration = s.Duration,
                                   MinQuentity = (int)s.MinQuentity,
                                   Description = s.Description,
                                   PacketSize = s.PacketSize,
                                   GrowthHabit = s.GrowthHabit,
                                   PositiveFactor = s.PositiveFactor,
                                   NegativeFactor = s.NegativeFactor,
                                   Temp = s.Temp,
                                   Fertilizer = s.Fertilizer,
                                   Height = s.Height,
                                   SeedsPerPacket = (int)s.SeedsPerPacket,
                                   SeedImages = SeedImagesList
                               }).FirstOrDefault();
            return View(seedDetails);
        }

        [HttpGet]
        public ActionResult Pesticides()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            var pesticideList = db.Pesticides.Select(x => new ManagePesticides
            {
                id = x.id,
                PesticideName = x.PesticideName,
                Category = x.Category,
                Image = x.PesticideImage,
                Price = (decimal)x.Price
            }).ToList();
            return View(pesticideList);
        }

        [HttpGet]
        public ActionResult PesticideDetail(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            var pesticideDetails = (from s in db.Pesticides
                                    where s.id == id
                                    select new AddOrEditPesticide()
                                    {
                                        id = s.id,
                                        PesticideName = s.PesticideName,
                                        Category = s.Category,
                                        PesticideImage = s.PesticideImage,
                                        PackingType = s.PackingType,
                                        Dosage = s.Dosage,
                                        Formulation = s.Formulation,
                                        PesticideDescription = s.PesticideDescription
                                    }).FirstOrDefault();
            return View(pesticideDetails);
        }

        [HttpGet]
        public ActionResult Fertilizers()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            var fertilizerList = db.Fertilizers.Select(x => new ManageFertilizers
            {
                id = x.id,
                FertilizerName = x.FertilizerName,
                Material = x.Material,
                Image = x.FertilizerImage,
                Price = (decimal)x.Price
            }).ToList();
            return View(fertilizerList);
        }

        [HttpGet]
        public ActionResult FertilizerDetail(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            var fertilizerDetails = (from s in db.Fertilizers
                                    where s.id == id
                                    select new AddOrEditFertilizer()
                                    {
                                        id = s.id,
                                        FertilizerName = s.FertilizerName,
                                        Material = s.Material,
                                        FertilizerImage = s.FertilizerImage,
                                        PackSize = s.PackSize,
                                        PackType = s.PackType,
                                        Features = s.Features,
                                        Solubility = s.Solubility,
                                        Doses = s.Doses,
                                        PHvalue = s.PHvalue,
                                        HowToUse = s.HowToUse,
                                        Price = (decimal)s.Price
                                    }).FirstOrDefault();
            return View(fertilizerDetails);
        }

        public ActionResult DailyPrice()
        {
            return View();
        }

        public ActionResult TractorCompare()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var brandList = db.Users.Where(x => x.role_id == 3).Select(x => new { x.id, x.Name }).ToList();
            ViewBag.brands = brandList;
            return View();
        }

        [HttpGet]
        public JsonResult GetTractorModels(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var tractorModels = db.Tractors.Where(x => x.dealer_id == id).Select(x => new { x.id, x.ModelName }).ToList();
            return Json(tractorModels, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTractorDetails(string[] ids)
        {
            List<int> idList = new List<int>();
            List<CompareTractors> tractorModels = new List<CompareTractors>();
            for (int i = 0; i < ids.Length; i++)
            {
                idList.Add(int.Parse(ids[i]));
            }
            int id1 = idList[0];
            int id2 = idList[1];

            SmartFarmingEntities db = new SmartFarmingEntities();
            var tractorModel1 = db.Tractors.Where(x => x.id == id1).Select(s => new CompareTractors
            {
                id = s.id,
                DealerName = db.Users.Where(x => x.id == s.dealer_id).FirstOrDefault().Name,
                ModelName = s.ModelName,
                TractorImage = s.TractorImage,
                Capacity = (int)s.Capacity,
                Clutch = s.Clutch,
                Price = s.Price,
                NoOfCylinder = (int)s.NoOfCylinder,
                HpCategory = (int)s.HpCategory,
                EngineRatedRPM = s.EngineRatedRPM,
                Cooling = s.Cooling,
                AirFilter = s.AirFilter,
                Brake = s.Brake,
                Battery = s.Battery,
                GearBox = s.GearBox,
                Steering = s.Steering,
                FuelTank = s.FuelTank,
                Features = s.Features,
                Accessories = s.Accessories,
                Warranty = (int)s.Warranty,
                Status = s.Status,
                TotalWeight = (int)s.TotalWeight,
                GroundClearance = (int)s.GroundClearance,
                TurningRadius = (int)s.TurningRadius
            }).FirstOrDefault();

            var tractorModel2 = db.Tractors.Where(x => x.id == id2).Select(s => new CompareTractors
            {
                id = s.id,
                DealerName = db.Users.Where(x => x.id == s.dealer_id).FirstOrDefault().Name,
                ModelName = s.ModelName,
                TractorImage = s.TractorImage,
                Capacity = (int)s.Capacity,
                Clutch = s.Clutch,
                Price = s.Price,
                NoOfCylinder = (int)s.NoOfCylinder,
                HpCategory = (int)s.HpCategory,
                EngineRatedRPM = s.EngineRatedRPM,
                Cooling = s.Cooling,
                AirFilter = s.AirFilter,
                Brake = s.Brake,
                Battery = s.Battery,
                GearBox = s.GearBox,
                Steering = s.Steering,
                FuelTank = s.FuelTank,
                Features = s.Features,
                Accessories = s.Accessories,
                Warranty = (int)s.Warranty,
                Status = s.Status,
                TotalWeight = (int)s.TotalWeight,
                GroundClearance = (int)s.GroundClearance,
                TurningRadius = (int)s.TurningRadius
            }).FirstOrDefault();
            tractorModels.Add(tractorModel1);
            tractorModels.Add(tractorModel2);
            if (idList.Count() > 2)
            {
                int id3 = idList[2];
                var tractorModel3 = db.Tractors.Where(x => x.id == id3).Select(s => new CompareTractors
                {
                    id = s.id,
                    DealerName = db.Users.Where(x => x.id == s.dealer_id).FirstOrDefault().Name,
                    ModelName = s.ModelName,
                    TractorImage = s.TractorImage,
                    Capacity = (int)s.Capacity,
                    Clutch = s.Clutch,
                    Price = s.Price,
                    NoOfCylinder = (int)s.NoOfCylinder,
                    HpCategory = (int)s.HpCategory,
                    EngineRatedRPM = s.EngineRatedRPM,
                    Cooling = s.Cooling,
                    AirFilter = s.AirFilter,
                    Brake = s.Brake,
                    Battery = s.Battery,
                    GearBox = s.GearBox,
                    Steering = s.Steering,
                    FuelTank = s.FuelTank,
                    Features = s.Features,
                    Accessories = s.Accessories,
                    Warranty = (int)s.Warranty,
                    Status = s.Status,
                    TotalWeight = (int)s.TotalWeight,
                    GroundClearance = (int)s.GroundClearance,
                    TurningRadius = (int)s.TurningRadius
                }).FirstOrDefault();
                tractorModels.Add(tractorModel3);
            }
            return Json(tractorModels);
        }
        public ActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUs model)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            ContactU contactModel = new ContactU()
            {
                Name = model.Name,
                ContactNo = model.ContactNo,
                Email = model.Email,
                Subject = model.Subject,
                Text = model.Text
            };

            db.ContactUs.Add(contactModel);
            db.SaveChanges();
            TempData["Submit"] = "Your Response is Submitted!";
            return RedirectToAction("ContactUs", "Home");
        }

        [HttpPost]
        public void AddQuatation(AddQuatation model)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            Quotation quatationModel = new Quotation()
            {
                seed_id = model.seed_id,
                dealer_id = model.dealer_id,
                Quantity = model.quantity,
                Unit = model.noOfSeed,
                ContactNo = model.contactNo
            };
            db.Quotations.Add(quatationModel);
            db.SaveChanges();
        }
        [HttpGet]
        public ActionResult News()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var newsList = (from c in db.News
                            select new NewsView
                            {
                                id = c.id,
                                Headline = c.Headline,
                                Text = c.Headline,
                                Link = c.Link,
                                Date = c.Date
                            }).ToList();
            return View(newsList);
        }
        [HttpGet]
        public ActionResult NewsDetails(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var newsList = (from c in db.News
                            where c.id == id
                            select new NewsView
                            {
                                id = c.id,
                                Headline = c.Headline,
                                Text = c.Text,
                                Link = c.Link,
                                Date = c.Date
                            }).FirstOrDefault();
            return View(newsList);
        }
    }
}