using App.Application.Features.Products;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using CleanApp.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : CustomBaseController // bu custom controllerdan miras aldık ki sürekli custom codda yer
                                                                                           // alan metotları hep aynı şekilde kontrolünü yapıp dönücez ya ondan
    {
        // create yaptığım yerde ben response olarak 201 döndüğümde o yeni eklenen dataya nasıl erişebileceğimi gösteren bir url yi o response un headerına 
        // ekleyebiliyorum. 
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllAsync());
        
        [HttpGet("{pageNumber:int}/{pageSize:int}")] // int diyerek route constraint yaptık yani gelecek olan değer int olmalı dedik
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await productService.GetPagedAllListAsync(pageNumber, pageSize));
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));
        // {
        //     var serviceResult = await productService.GetByIdAsync(id);
        //     
        //
        //     // if (serviceResult.Status == HttpStatusCode.NoContent)
        //     // {
        //     //     return new ObjectResult(null) // eğer boş ise np content ve null olarak döndük
        //     //     {
        //     //         StatusCode = (int)serviceResult.Status,
        //     //     };
        //     // }
        //     //
        //     // return new ObjectResult(serviceResult)
        //     // {
        //     //     StatusCode = (int)serviceResult.Status,
        //     // };
        // }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest createProductRequest) => CreateActionResult(await productService.CreateAsync(createProductRequest));
        
        [ServiceFilter(typeof(NotFoundFilter<Product,int>))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest updateProductRequest) => CreateActionResult(await productService.UpdateAsync(id, updateProductRequest));
        
        
        [HttpPatch("Stock")]
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateStockAsync(request));
        
        
        [ServiceFilter(typeof(NotFoundFilter<Product,int>))] // bu filterda constructorında parametre aldığı için böyle oluyor
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));
    }
}
