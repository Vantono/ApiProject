using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Common.DTOs;
using CourseProject.Common.DTOs.Address;

namespace CourseProject.Common.Interfaces;

public interface IAddressService
{
    Task<int> CreateAddressAsync(AddressCreate addressCreate);
    Task UpdateAddressAsync(AddressUpdate adressUpdate);
    Task DeleteAddressAsync(AddressDelete addressDelete);
    Task<AddressGet> GetAddressAsync(int id);
    Task<List<AddressGet>> GetAddressesAsync();
}
