using SmartFarming.Models;
using SmartFarming.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartFarming.Controllers
{
    
    public class DealerController : Controller
    {
        [AuthLog(Roles = "Dealer")]
        [HttpGet]
        public ActionResult ManageSeeds()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var dealerId = Convert.ToInt32(HttpContext.Session["UserId"]);
            var seedList = (from m in db.Seeds
                            join c in db.Crops on m.crop_id equals c.id
                            where m.dealer_id == dealerId
                            select new ManageSeeds()
                            {
                                id = m.id,
                                SeedName = m.SeedName,
                                CropName = c.CropName,
                                Price = (decimal)m.Price
                            }).ToList();
            return View(seedList);
        }
        [AuthLog(Roles = "Dealer")]
        [HttpGet]
        public ActionResult AddOrEditSeed(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            AddOrEditSeed model = new AddOrEditSeed();

            if (id != 0)
            {
                var seedDetails = (from s in db.Seeds
                                   where s.id == id
                                   select new AddOrEditSeed()
                                   {
                                       id = s.id,
                                       crop_id = s.crop_id,
                                       CropList = db.Crops.Select(m => new CropList
                                       {
                                           id = m.id,
                                           Name = m.CropName
                                       }).ToList(),
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
                                       SeedsPerPacket = (int)s.SeedsPerPacket
                                   }).FirstOrDefault();
                var SeedImages = db.SeedImages.Where(m => m.seed_id == id).Select(m => m.Image.ToString()).ToList();
                if (SeedImages.Any())
                {
                    seedDetails.SeedImages = SeedImages;
                }
                return View(seedDetails);
            }
            else
            {
                model.id = 0;
                model.CropList = db.Crops.Select(m => new CropList
                {
                    id = m.id,
                    Name = m.CropName
                }).ToList();
                return View(model);
            }
        }
        [AuthLog(Roles = "Dealer")]
        [HttpPost]
        public ActionResult AddOrEditSeed(AddOrEditSeed model)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            if (model.id != 0)
            {
                var seedDetails = db.Seeds.Where(x => x.id == model.id).FirstOrDefault();

                seedDetails.crop_id = model.crop_id;
                seedDetails.dealer_id = Convert.ToInt32(HttpContext.Session["UserId"]);
                seedDetails.SeedName = model.SeedName;
                seedDetails.Season = model.Season;
                seedDetails.PestTolerance = model.PestTolerance;
                seedDetails.Price = model.Price;
                seedDetails.Duration = model.Duration;
                seedDetails.MinQuentity = model.MinQuentity;
                seedDetails.Description = model.Description;
                seedDetails.PacketSize = model.PacketSize;
                seedDetails.GrowthHabit = model.GrowthHabit;
                seedDetails.PositiveFactor = model.PositiveFactor;
                seedDetails.NegativeFactor = model.NegativeFactor;
                seedDetails.Temp = model.Temp;
                seedDetails.Fertilizer = model.Fertilizer;
                seedDetails.Height = model.Height;
                seedDetails.SeedsPerPacket = model.SeedsPerPacket;

                db.SaveChanges();

                foreach (HttpPostedFileBase file in model.Images)
                {
                    if(file != null)
                    {
                        var imageName = file.FileName;
                        var path = Path.Combine(Server.MapPath("~/Images/"), imageName);
                        file.SaveAs(path);

                        SeedImage seedImages = new SeedImage()
                        {
                            seed_id = model.id,
                            Image = imageName
                        };
                        db.SeedImages.Add(seedImages);
                        db.SaveChanges();
                    }
                    
                }
                  
            }
            else
            {

                Seed seedDetails = new Seed()
                {
                    crop_id = model.crop_id,
                    dealer_id = Convert.ToInt32(HttpContext.Session["UserId"]),
                    SeedName = model.SeedName,
                    Season = model.Season,
                    PestTolerance = model.PestTolerance,
                    Price = model.Price,
                    Duration = model.Duration,
                    MinQuentity = model.MinQuentity,
                    Description = model.Description,
                    PacketSize = model.PacketSize,
                    GrowthHabit = model.GrowthHabit,
                    PositiveFactor = model.PositiveFactor,
                    NegativeFactor = model.NegativeFactor,
                    Temp = model.Temp,
                    Fertilizer = model.Fertilizer,
                    Height = model.Height,
                    SeedsPerPacket = model.SeedsPerPacket
                };
                db.Seeds.Add(seedDetails);
                db.SaveChanges();

                foreach (HttpPostedFileBase file in model.Images)
                {
                    var imageName = file.FileName;
                    var path = Path.Combine(Server.MapPath("~/Images/"), imageName);
                    file.SaveAs(path);

                    SeedImage seedImages = new SeedImage()
                    {
                        seed_id = seedDetails.id,
                        Image = imageName
                };
            }

        }

            return RedirectToAction("ManageSeeds");
        }
        [AuthLog(Roles = "Dealer")]
        [HttpGet]
        public ActionResult DeleteSeed(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            var seedDetails = db.Seeds.Where(m => m.id == id).FirstOrDefault();
            var seedImages = db.SeedImages.Where(m => m.seed_id == id).ToList();
            var seedFaq = db.SeedFaqs.Where(m => m.seed_id == id).ToList();
            var quatation = db.Quotations.Where(m => m.seed_id == id).ToList();
            foreach (var item in quatation)
            {
                db.Quotations.Remove(item);
                db.SaveChanges();
            }
            if (seedImages.Any())
            {
                foreach (var item in seedImages)
                {
                    db.SeedImages.Remove(item);
                }
            }
            if (seedFaq.Any())
            {
                foreach (var item in seedFaq)
                {
                    db.SeedFaqs.Remove(item);
                }
            }
            db.Seeds.Remove(seedDetails);
            db.SaveChanges();
            return RedirectToAction("ManageSeeds");
        }

        [AuthLog(Roles = "Dealer")]
        [HttpGet]
        public ActionResult ManagePesticides()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var dealerId = Convert.ToInt32(HttpContext.Session["UserId"]);
            var pesticideList = (from m in db.Pesticides
                            join c in db.Crops on m.crop_id equals c.id
                            where m.dealer_id == dealerId
                            select new ManagePesticides()
                            {
                                id = m.id,
                                PesticideName = m.PesticideName,
                                Category = m.Category,
                                CropName = c.CropName,
                                Price = (decimal)m.Price
                            }).ToList();
            return View(pesticideList);
        }

        [AuthLog(Roles = "Dealer")]
        [HttpGet]
        public ActionResult AddOrEditPesticide(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            AddOrEditPesticide model = new AddOrEditPesticide();

            if (id != 0)
            {
                var pesticideDetails = (from s in db.Pesticides
                                   where s.id == id
                                   select new AddOrEditPesticide()
                                   {
                                       id = s.id,
                                       crop_id = s.crop_id,
                                       CropList = db.Crops.Select(m => new CropList
                                       {
                                           id = m.id,
                                           Name = m.CropName
                                       }).ToList(),
                                       PesticideName = s.PesticideName,
                                       Category = s.Category,
                                       PesticideImage = s.PesticideImage,
                                       State = s.State,
                                       PackingType = s.PackingType,
                                       Dosage = s.Dosage,
                                       Formulation = s.Formulation,
                                       PesticideDescription = s.PesticideDescription,
                                       Price = (decimal)s.Price
                                   }).FirstOrDefault();
                return View(pesticideDetails);
            }
            else
            {
                model.id = 0;
                model.CropList = db.Crops.Select(m => new CropList
                {
                    id = m.id,
                    Name = m.CropName
                }).ToList();
                return View(model);
            }
        }

        [AuthLog(Roles = "Dealer")]
        [HttpPost]
        public ActionResult AddOrEditPesticide(AddOrEditPesticide model, HttpPostedFileBase file)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            if (file != null)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/"), filename);
                file.SaveAs(path);
                model.PesticideImage = filename;
            }

            if (model.id != 0)
            {
                var pesticideDetails = db.Pesticides.Where(x => x.id == model.id).FirstOrDefault();

                pesticideDetails.crop_id = model.crop_id;
                pesticideDetails.dealer_id = Convert.ToInt32(HttpContext.Session["UserId"]);
                pesticideDetails.PesticideName = model.PesticideName;
                pesticideDetails.Category = model.Category;
                pesticideDetails.PesticideImage = model.PesticideImage;
                pesticideDetails.State = model.State;
                pesticideDetails.PackingType = model.PackingType;
                pesticideDetails.Dosage = model.Dosage;
                pesticideDetails.Formulation = model.Formulation;
                pesticideDetails.PesticideDescription = model.PesticideDescription;
                pesticideDetails.Price = model.Price;

                db.SaveChanges();
            }
            else
            {
                Pesticide pesticideDetails = new Pesticide()
                {
                    crop_id = model.crop_id,
                    dealer_id = Convert.ToInt32(HttpContext.Session["UserId"]),
                    PesticideName = model.PesticideName,
                    Category = model.Category,
                    PesticideImage = model.PesticideImage,
                    State = model.State,
                    PackingType = model.PackingType,
                    Dosage = model.Dosage,
                    Formulation = model.Formulation,
                    PesticideDescription = model.PesticideDescription,
                    Price = model.Price
                };
                    
                db.Pesticides.Add(pesticideDetails);
                db.SaveChanges();

            }
            return RedirectToAction("ManagePesticides");
        }

        [AuthLog(Roles = "Dealer")]
        [HttpGet]
        public ActionResult DeletePesticide(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            var pesticideDetails = db.Pesticides.Where(m => m.id == id).FirstOrDefault();
            var quatation = db.Quotations.Where(m => m.pesticide_id == id).ToList();
            foreach (var item in quatation)
            {
                db.Quotations.Remove(item);
                db.SaveChanges();
            }
            db.Pesticides.Remove(pesticideDetails);
            db.SaveChanges();
            return RedirectToAction("ManagePesticides");
        }

        [AuthLog(Roles = "Dealer")]
        [HttpGet]
        public ActionResult ManageFertilizers()
        { 
            SmartFarmingEntities db = new SmartFarmingEntities();
            var dealerId = Convert.ToInt32(HttpContext.Session["UserId"]);
            var fertilizerList = (from m in db.Fertilizers
                                 join c in db.Crops on m.crop_id equals c.id
                                 where m.dealer_id == dealerId
                                 select new ManageFertilizers()
                                 {
                                     id = m.id,
                                     FertilizerName = m.FertilizerName,
                                     Material = m.Material,
                                     CropName = c.CropName,
                                     Price = (decimal)m.Price
                                 }).ToList();
            return View(fertilizerList);
        }

        [AuthLog(Roles = "Dealer")]
        [HttpGet]
        public ActionResult AddOrEditFertilizer(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            AddOrEditFertilizer model = new AddOrEditFertilizer();

            if (id != 0)
            {
                var fertilizerDetails = (from s in db.Fertilizers
                                        where s.id == id
                                        select new AddOrEditFertilizer()
                                        {
                                            id = s.id,
                                            crop_id = s.crop_id,
                                            CropList = db.Crops.Select(m => new CropList
                                            {
                                                id = m.id,
                                                Name = m.CropName
                                            }).ToList(),
                                            FertilizerName = s.FertilizerName,
                                            FertilizerImage = s.FertilizerImage,
                                            Material = s.Material,
                                            PackType = s.PackType,
                                            PackSize = s.PackSize,
                                            Features = s.Features,
                                            Solubility = s.Solubility,
                                            Doses = s.Doses,
                                            PHvalue = s.PHvalue,
                                            HowToUse = s.HowToUse,
                                            Price = (decimal)s.Price
                                        }).FirstOrDefault();
                return View(fertilizerDetails);
            }
            else
            {
                model.id = 0;
                model.CropList = db.Crops.Select(m => new CropList
                {
                    id = m.id,
                    Name = m.CropName
                }).ToList();
                return View(model);
            }
        }

        [AuthLog(Roles = "Dealer")]
        [HttpPost]
        public ActionResult AddOrEditFertilizer(AddOrEditFertilizer model, HttpPostedFileBase file)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            if (file != null)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/"), filename);
                file.SaveAs(path);
                model.FertilizerImage = filename;
            }

            if (model.id != 0)
            {
                var fertilizerDetails = db.Fertilizers.Where(x => x.id == model.id).FirstOrDefault();

                fertilizerDetails.crop_id = model.crop_id;
                fertilizerDetails.dealer_id = Convert.ToInt32(HttpContext.Session["UserId"]);
                fertilizerDetails.FertilizerName = model.FertilizerName;
                fertilizerDetails.Material = model.Material;
                fertilizerDetails.FertilizerImage = model.FertilizerImage;
                fertilizerDetails.PackType = model.PackType;
                fertilizerDetails.PackSize = model.PackSize;
                fertilizerDetails.Features = model.Features;
                fertilizerDetails.Solubility = model.Solubility;
                fertilizerDetails.Doses = model.Doses;
                fertilizerDetails.PHvalue = model.PHvalue;
                fertilizerDetails.HowToUse = model.HowToUse;
                fertilizerDetails.Price = model.Price;

                db.SaveChanges();
            }
            else
            {
                Fertilizer fertilizerDetails = new Fertilizer()
                {
                    crop_id = model.crop_id,
                    dealer_id = Convert.ToInt32(HttpContext.Session["UserId"]),
                    FertilizerName = model.FertilizerName,
                    Material = model.Material,
                    FertilizerImage = model.FertilizerImage,
                    PackType = model.PackType,
                    PackSize = model.PackSize,
                    Features = model.Features,
                    Solubility = model.Solubility,
                    Doses = model.Doses,
                    PHvalue = model.PHvalue,
                    HowToUse = model.HowToUse,
                    Price = model.Price
                    
                };

                db.Fertilizers.Add(fertilizerDetails);
                db.SaveChanges();

            }
            return RedirectToAction("ManageFertilizers");
        }

        [AuthLog(Roles = "Dealer")]
        [HttpGet]
        public ActionResult DeleteFertilizer(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            var fertilizerDetails = db.Fertilizers.Where(m => m.id == id).FirstOrDefault();
            var quatation = db.Quotations.Where(m => m.fertilizer_id == id).ToList();
            foreach (var item in quatation)
            {
                db.Quotations.Remove(item);
                db.SaveChanges();
            }
            
            db.Fertilizers.Remove(fertilizerDetails);
            db.SaveChanges();
            return RedirectToAction("ManageFertilizers");
        }

        [AuthLog(Roles = "Dealer")]
        [HttpGet]
        public ActionResult Quatation()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var dealerId = Convert.ToInt32(HttpContext.Session["UserId"]);

            Quatation model = new Quatation();

            var seedlist = (from m in db.Quotations
                                 join s in db.Seeds on m.seed_id equals s.id
                                 where m.dealer_id == dealerId
                                 select new QuatationSeed()
                                 {
                                     id = m.id,
                                     SeedName = s.SeedName,
                                     Quantity = m.Quantity,
                                     Unit = m.Unit,
                                     ContactNo = m.ContactNo
                                 }).ToList();
            var pesticideList = (from m in db.Quotations
                                 join p in db.Pesticides on m.pesticide_id equals p.id
                                 where m.dealer_id == dealerId
                                 select new QuatationPesticide()
                                 {
                                     id = m.id,
                                     PesticideName = p.PesticideName,
                                     Quantity = m.Quantity,
                                     Unit = m.Unit,
                                     ContactNo = m.ContactNo
                                 }).ToList();
            var fertilizerList = (from m in db.Quotations
                                  join f in db.Fertilizers on m.fertilizer_id equals f.id
                                  where m.dealer_id == dealerId
                                 select new QuatationFertilizer()
                                 {
                                     id = m.id,
                                     FertilizerName = f.FertilizerName,
                                     Quantity = m.Quantity,
                                     Unit = m.Unit,
                                     ContactNo = m.ContactNo
                                 }).ToList();
            model.seedlist = seedlist;
            model.pesticidelist = pesticideList;
            model.fertilizerlist = fertilizerList;
            return View(model);
        }

        [AuthLog(Roles = "Tractor")]
        [HttpGet]
        public ActionResult ManageTractors()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var dealerId = Convert.ToInt32(HttpContext.Session["UserId"]);
            var tractorList = (from t in db.Tractors
                            where t.dealer_id == dealerId
                            select new ManageTractor()
                            {
                                id = t.id,
                                ModelName = t.ModelName,
                                Warrenty = (int)t.Warranty,
                                Price = (decimal)t.Price,
                                Status = t.Status
                            }).ToList();
            return View(tractorList);
        }

        [AuthLog(Roles = "Tractor")]
        [HttpGet]
        public ActionResult AddOrEditTractor(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            AddOrEditTractor model = new AddOrEditTractor();

            if (id != 0)
            {
                var tractorDetails = (from s in db.Tractors
                                   where s.id == id
                                   select new AddOrEditTractor()
                                   {
                                       id = s.id,
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
                return View(tractorDetails);
            }
            else
            {
                model.id = 0;
                return View(model);
            }
        }

        [AuthLog(Roles = "Tractor")]
        [HttpPost]
        public ActionResult AddOrEditTractor(AddOrEditTractor model, HttpPostedFileBase file)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            if (file != null)
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/"), filename);
                file.SaveAs(path);
                model.TractorImage = filename;
            }
            if (model.id != 0)
            {
                
                var tractorDetails = db.Tractors.Where(x => x.id == model.id).FirstOrDefault();

                tractorDetails.ModelName = model.ModelName;
                tractorDetails.TractorImage = model.TractorImage;
                tractorDetails.Capacity = model.Capacity;
                tractorDetails.Clutch = model.Clutch;
                tractorDetails.Price = model.Price;
                tractorDetails.NoOfCylinder = model.NoOfCylinder;
                tractorDetails.HpCategory = model.HpCategory;
                tractorDetails.EngineRatedRPM = model.EngineRatedRPM;
                tractorDetails.Cooling = model.Cooling;
                tractorDetails.AirFilter = model.AirFilter;
                tractorDetails.Brake = model.Brake;
                tractorDetails.Battery = model.Battery;
                tractorDetails.GearBox = model.GearBox;
                tractorDetails.Steering = model.Steering;
                tractorDetails.FuelTank = model.FuelTank;
                tractorDetails.Accessories = model.Accessories;
                tractorDetails.Features = model.Features;
                tractorDetails.Warranty = model.Warranty;
                tractorDetails.Status = model.Status;
                tractorDetails.TotalWeight = model.TotalWeight;
                tractorDetails.GroundClearance = model.GroundClearance;
                tractorDetails.TurningRadius = model.TurningRadius;

                db.SaveChanges();
            }
            else
            {

                Tractor tractorDetails = new Tractor()
                {
                    dealer_id = Convert.ToInt32(HttpContext.Session["UserId"]),
                    ModelName = model.ModelName,
                    TractorImage = model.TractorImage,
                    Capacity = model.Capacity,
                    Clutch = model.Clutch,
                    Price = model.Price,
                    NoOfCylinder = model.NoOfCylinder,
                    HpCategory = model.HpCategory,
                    EngineRatedRPM = model.EngineRatedRPM,
                    Cooling = model.Cooling,
                    AirFilter = model.AirFilter,
                    Brake = model.Brake,
                    Battery = model.Battery,
                    GearBox = model.GearBox,
                    Steering = model.Steering,
                    FuelTank = model.FuelTank,
                    Features = model.Features,
                    Accessories = model.Accessories,
                    Warranty = model.Warranty,
                    Status = model.Status,
                    TotalWeight = model.TotalWeight,
                    GroundClearance = model.GroundClearance,
                    TurningRadius = model.TurningRadius

            };
                db.Tractors.Add(tractorDetails);
                db.SaveChanges();
            }

            return RedirectToAction("ManageTractors");
        }

        [AuthLog(Roles = "Tractor")]
        [HttpGet]
        public ActionResult DeleteTractor(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            var tractorDetails = db.Tractors.Where(m => m.id == id).FirstOrDefault();

            db.Tractors.Remove(tractorDetails);
            db.SaveChanges();
            return RedirectToAction("ManageTractors");
        }
    }
}