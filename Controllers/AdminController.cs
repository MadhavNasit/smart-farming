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
    [AuthLog(Roles = "Admin")]
    public class AdminController : Controller
    {
        
        [HttpGet]
        public ActionResult ManageDealer()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var usersdetails = (from x in db.Users
                                join y in db.Roles on x.role_id equals y.id
                                select new ManageDealer
                                {
                                    Id = x.id,
                                    Name = x.Name,
                                    Email = x.Email,
                                    Title = y.Title
                                }).ToList();
            return View(usersdetails);
        }

        [HttpGet]
        public ActionResult AddDealer()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            AddDealer model = new AddDealer()
            {
                Roles = db.Roles.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddDealer(AddDealer model)
        {
            User usermodel = new User()
            {
                Name = model.Name,
                Email = model.Email,
                role_id = model.role_id
            };

            using (SmartFarmingEntities dbcrop = new SmartFarmingEntities())
            {
                dbcrop.Users.Add(usermodel);
                dbcrop.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SucessMessage = "Item Added Sucessful.";

            return RedirectToAction("ManageDealer");
        }

        [HttpGet]
        public ActionResult DeleteDealer(int id)
        {
            using (SmartFarmingEntities db = new SmartFarmingEntities())
            {
                User model = db.Users.Where(x => x.id == id).FirstOrDefault();
                List<Fertilizer> fertilizers = db.Fertilizers.Where(x => x.dealer_id == id).ToList();
                List<Pesticide> pesticides = db.Pesticides.Where(x => x.dealer_id == id).ToList();
                List<Quotation> quotations = db.Quotations.Where(x => x.dealer_id == id).ToList();
                List<Seed> seedList = db.Seeds.Where(x => x.dealer_id == id).ToList();
                List<Tractor> tractors = db.Tractors.Where(x => x.dealer_id == id).ToList();
                db.Users.Remove(model);
                db.SaveChanges();

                return RedirectToAction("ManageDealer");
            }
        }

        [HttpGet]
        public ActionResult ManageCrops()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            ManageCrop model = new ManageCrop();
            var cropList = (from m in db.Crops
                            join t in db.CropTypes on m.type_id equals t.id
                            select new ManageCrop()
                            {
                                id = m.id,
                                CropName = m.CropName,
                                Kharif = (bool)m.Kharif,
                                Rabi = (bool)m.Rabi,
                                Zaid = (bool)m.Zaid,
                                Type = t.Croptype1
                            }).ToList();

            foreach (var item in cropList)
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
                model.Season = string.Join(", ", season);
                item.Season = model.Season;
            }

            return View(cropList);
        }

        [HttpGet]
        public ActionResult AddCrop(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            AddCrop model = new AddCrop();


            if (id != 0)
            {
                var cropDetails = (from c in db.Crops
                                   where c.id == id
                                   select new AddCrop()
                                   {
                                       id = c.id,
                                       CropName = c.CropName,
                                       CropDesc = c.CropDesc,
                                       Kharif = (bool)c.Kharif,
                                       Rabi = (bool)c.Rabi,
                                       Zaid = (bool)c.Zaid,
                                       type_id = c.type_id,
                                       CropImage = c.CropImage,
                                       Temp = c.Temp,
                                       RainFall = c.RainFall,
                                       SoilType = c.SoilType,
                                       Types = db.CropTypes.ToList(),
                                       States = db.States.ToList(),
                                       HighestProducers = (int)c.HighestProducers,
                                       LandPreparation = c.LandPreparation,
                                       Harvesting = c.Harvesting,
                                       SowingMethod = c.SowingMethod
                                   }).FirstOrDefault();
                return View(cropDetails);
            }
            else
            {
                model.id = 0;
                model.Types = db.CropTypes.ToList();
                model.States = db.States.ToList();
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult AddCrop(AddCrop model, HttpPostedFileBase file)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

            if (model.id != 0)
            {
                if (file != null)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), filename);
                    file.SaveAs(path);
                    model.CropImage = filename;
                }
                var cropDetails = db.Crops.Where(x => x.id == model.id).FirstOrDefault();

                cropDetails.CropName = model.CropName;
                cropDetails.CropDesc = model.CropDesc;
                cropDetails.Kharif = (bool)model.Kharif;
                cropDetails.Rabi = (bool)model.Rabi;
                cropDetails.Zaid = (bool)model.Zaid;
                cropDetails.type_id = model.type_id;
                cropDetails.CropImage = model.CropImage;
                cropDetails.Temp = model.Temp;
                cropDetails.RainFall = model.RainFall;
                cropDetails.SoilType = model.SoilType;
                cropDetails.HighestProducers = (int)model.HighestProducers;
                cropDetails.SowingMethod = model.SowingMethod;
                cropDetails.Harvesting = model.Harvesting;
                cropDetails.LandPreparation = model.LandPreparation;

                db.SaveChanges();
            }
            else
            {
                var filename = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/"), filename);
                file.SaveAs(path);
                model.CropImage = filename;

                Crop cropDetails = new Crop()
                {
                    CropName = model.CropName,
                    CropDesc = model.CropDesc,
                    Kharif = model.Kharif,
                    Rabi = model.Rabi,
                    Zaid = model.Zaid,
                    type_id = model.type_id,
                    CropImage = model.CropImage,
                    Temp = model.Temp,
                    RainFall = model.RainFall,
                    SoilType = model.SoilType,
                    HighestProducers = model.HighestProducers,
                    LandPreparation = model.LandPreparation,
                    Harvesting = model.Harvesting,
                    SowingMethod = model.SowingMethod
                };
                db.Crops.Add(cropDetails);
                db.SaveChanges();
            }


            if (model.MajorProducers[0] != "")
            {
                MajorProducer stateList = new MajorProducer();
                int cropId = db.Crops.Where(x => x.CropName == model.CropName).FirstOrDefault().id;
                var stateIds = model.MajorProducers[1].Split(',').ToList();
                for (int item = 0; item < stateIds.Count(); item++)
                {
                    stateList.crop_id = cropId;
                    stateList.state_id = Convert.ToInt32(stateIds[item]);
                    db.MajorProducers.Add(stateList);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ManageCrops");
        }
        [HttpGet]
        public ActionResult DeleteCrop(int id)
        {
            using (SmartFarmingEntities db = new SmartFarmingEntities())
            {
                Crop cropDetail = db.Crops.Where(m => m.id == id).FirstOrDefault();
                MajorProducer states = db.MajorProducers.Where(m => m.crop_id == id).FirstOrDefault();
                List<Seed> seedDetail = db.Seeds.Where(m => m.crop_id == id).ToList();
                List<Pesticide> pesticideCropDetails = db.Pesticides.Where(m => m.crop_id == id).ToList();
                List<Fertilizer> fertilizerCropDetails = db.Fertilizers.Where(m => m.crop_id == id).ToList();

                if (states != null)
                {
                    db.MajorProducers.Remove(states);
                }
                if (seedDetail != null)
                {
                    foreach (var item in seedDetail)
                    {
                        db.Seeds.Remove(item);
                    }
                }
                if (pesticideCropDetails != null)
                {
                    foreach (var item in pesticideCropDetails)
                    {
                        db.Pesticides.Remove(item);
                    }
                }
                if (fertilizerCropDetails != null)
                {
                    foreach (var item in fertilizerCropDetails)
                    {
                        db.Fertilizers.Remove(item);
                    }
                }
                db.Crops.Remove(cropDetail);
                db.SaveChanges();

                return RedirectToAction("ManageCrops");
            }
        }

        [HttpGet]
        public ActionResult ManageFaqs()
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var faqs = db.Faqs.Select(m => new ManageFaq
            {
                id = m.id,
                Question = m.Question,
                Answer = m.Answer
            }).ToList();
            return View(faqs);
        }
        [HttpGet]
        public ActionResult AddFaq(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            ManageFaq model = new ManageFaq();
            if (id != 0)
            {
                model = db.Faqs.Where(m => m.id == id).Select(m => new ManageFaq
                {
                    id = m.id,
                    Question = m.Question,
                    Answer = m.Answer
                }).FirstOrDefault();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddFaq(ManageFaq model)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            if (model.id != 0)
            {
                var faq = db.Faqs.Where(m => m.id == model.id).FirstOrDefault();
                faq.Question = model.Question;
                faq.Answer = model.Answer;
            }
            else
            {
                Faq faq = new Faq()
                {
                    Question = model.Question,
                    Answer = model.Answer
                };
                db.Faqs.Add(faq);
            }
            db.SaveChanges();
            return RedirectToAction("ManageFaqs");
        }
        [HttpGet]
        public ActionResult DeleteFaq(int id)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();
            var faq = db.Faqs.Where(m => m.id == id).FirstOrDefault();
            db.Faqs.Remove(faq);
            db.SaveChanges();
            return RedirectToAction("ManageFaqs");
        }
        [HttpGet]
        public ActionResult ManageNews()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ManageNews(NewsView model)
        {
            SmartFarmingEntities db = new SmartFarmingEntities();

                News news = new News()
                {
                    Headline = model.Headline,
                    Text = model.Text,
                    Link = model.Link,
                    Date = model.Date
                };
                db.News.Add(news);

            db.SaveChanges();
            return RedirectToAction("ManageNews");
        }
    }
}