using AutoMapper;
using GameChallenge.Core.DBEntities;
using GameChallenge.Web.EnpointModel;
using GameChallenge.Web.Model;

namespace GameChallenge.Web
{
    public class RegisterAutoMapperEntities : Profile
    {
        public RegisterAutoMapperEntities()
        {
            CreateMap<Customer, CustomerModel>();
            CreateMap<CreateProductCategoryRequest, ProductCategory>();
            CreateMap<CreateProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductCategory, ProductCategoryResponse>();
            CreateMap<UpdateProductRequest, Product>();

            CreateMap<Order, OrderListResponse>();
            CreateMap<Order, OrderDetailResponse> ();
            CreateMap<OrderItem, OrderItemResponse>();



        }
    }

    
}
