using ServiceLayer.Models;
using ServiceLayer.Requests;
using SharedLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AbstractsModel
{
    public interface IProductService
    {
        Task<CustomResponseDto<List<ProductViewModel>>> ProductList(string email);
        Task<string> Buy(PaymentRequest request);


    }
}
