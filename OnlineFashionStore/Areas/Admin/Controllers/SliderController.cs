using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFashionStore.Extensions;
using OnlineFashionStore.Models;
using OnlineFashionStore.Models.DataModels;

namespace OnlineFashionStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _context;
        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult GetSliders()
        {
            return View(_context.Sliders.ToList());
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Slider slider)
        {
            if (FileExtensions.IsImage(slider.ImgFile))
            {
                string nameImg = await FileExtensions.SaveAsync(slider.ImgFile, "sliders");
                slider.Image = nameImg;
                _context.Sliders.Add(slider);
                _context.SaveChanges();
            }
            else
            {
                return RedirectToAction("Add");
            }
            return RedirectToAction("GetSliders");
        }
        public IActionResult Delete(int id)
        {
            var p = _context.Sliders.Find(id);
            if (p != null)
            {
                _context.Sliders.Remove(p);
            }
            _context.SaveChanges();
            return RedirectToAction("GetSliders");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Slider = _context.Sliders.ToList();
            return View(_context.Sliders.Find(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Slider newSlider)
        {
            var slideritem = _context.Sliders.Find(newSlider.Id);
            if (slideritem != null)
            {
                if (newSlider.ImgFile != null)
                {
                    if (FileExtensions.IsImage(newSlider.ImgFile))
                    {
                        string nameImg = await FileExtensions.SaveAsync(newSlider.ImgFile, "sliders");
                        slideritem.ImgFile = newSlider.ImgFile;
                        slideritem.Image = nameImg;
                        
                    }
                    else
                    {
                        return RedirectToAction("Edit");
                    }
                }
               
                slideritem.Title = newSlider.Title;
                slideritem.Description = newSlider.Description;
                slideritem.Name = newSlider.Name;
                _context.SaveChanges();
                return RedirectToAction("GetSliders");
            }
            return RedirectToAction("Edit");
        }
    }
}

