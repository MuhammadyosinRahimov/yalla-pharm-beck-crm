using Microsoft.EntityFrameworkCore;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;
using YallaPharm.Domain.Entities;
using YallaPharm.Infrastructure.Data;

namespace YallaPharm.Application.Services;

public class ClientService : IClientService
{
    private readonly ApplicationDbContext _context;

    public ClientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClientDto> CreateAsync(ClientDto dto)
    {
        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
            FullName = dto.FullName,
            Street = dto.Street,
            Landmark = dto.Landmark,
            GeolocationOfClientAddress = dto.GeolocationOfClientAddress,
            PhoneNumber = dto.PhoneNumber,
            Age = dto.Age,
            SocialUsername = dto.SocialUsername,
            Language = dto.Language,
            HavingChildren = dto.HavingChildren,
            HavingElderly = dto.HavingElderly,
            Gender = dto.Gender,
            FamilyStatus = dto.FamilyStatus,
            EconomicStanding = dto.EconomicStanding,
            ChildrensAge = dto.ChildrensAge,
            Type = dto.Type,
            ContactType = dto.ContactType,
            TakeIntoAccount = dto.TakeIntoAccount,
            DiscountForClient = dto.DiscountForClient
        };

        if (dto.Addresses != null)
        {
            foreach (var addr in dto.Addresses)
            {
                client.Addresses.Add(new Address
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = client.Id,
                    Street = addr.Street,
                    Landmark = addr.Landmark,
                    City = addr.City,
                    GeolocationOfClientAddress = addr.GeolocationOfClientAddress
                });
            }
        }

        if (dto.Childrens != null)
        {
            foreach (var child in dto.Childrens)
            {
                client.Childrens.Add(new ClientChildren
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = client.Id,
                    FullName = child.FullName,
                    Age = child.Age,
                    Gender = child.Gender
                });
            }
        }

        if (dto.Adults != null)
        {
            foreach (var adult in dto.Adults)
            {
                client.Adults.Add(new ClientAdult
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = client.Id,
                    FullName = adult.FullName,
                    Age = adult.Age,
                    Gender = adult.Gender
                });
            }
        }

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return MapToDto(client);
    }

    public async Task<ClientDto> UpdateAsync(ClientDto dto)
    {
        var client = await _context.Clients
            .Include(c => c.Addresses)
            .Include(c => c.Childrens)
            .Include(c => c.Adults)
            .FirstOrDefaultAsync(c => c.Id == dto.Id);

        if (client == null)
            throw new KeyNotFoundException($"Client with ID {dto.Id} not found");

        client.FullName = dto.FullName;
        client.Street = dto.Street;
        client.Landmark = dto.Landmark;
        client.GeolocationOfClientAddress = dto.GeolocationOfClientAddress;
        client.PhoneNumber = dto.PhoneNumber;
        client.Age = dto.Age;
        client.SocialUsername = dto.SocialUsername;
        client.Language = dto.Language;
        client.HavingChildren = dto.HavingChildren;
        client.HavingElderly = dto.HavingElderly;
        client.Gender = dto.Gender;
        client.FamilyStatus = dto.FamilyStatus;
        client.EconomicStanding = dto.EconomicStanding;
        client.ChildrensAge = dto.ChildrensAge;
        client.Type = dto.Type;
        client.ContactType = dto.ContactType;
        client.TakeIntoAccount = dto.TakeIntoAccount;
        client.DiscountForClient = dto.DiscountForClient;

        // Update addresses
        _context.Addresses.RemoveRange(client.Addresses);
        if (dto.Addresses != null)
        {
            foreach (var addr in dto.Addresses)
            {
                client.Addresses.Add(new Address
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = client.Id,
                    Street = addr.Street,
                    Landmark = addr.Landmark,
                    City = addr.City,
                    GeolocationOfClientAddress = addr.GeolocationOfClientAddress
                });
            }
        }

        // Update children
        _context.ClientChildrens.RemoveRange(client.Childrens);
        if (dto.Childrens != null)
        {
            foreach (var child in dto.Childrens)
            {
                client.Childrens.Add(new ClientChildren
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = client.Id,
                    FullName = child.FullName,
                    Age = child.Age,
                    Gender = child.Gender
                });
            }
        }

        // Update adults
        _context.ClientAdults.RemoveRange(client.Adults);
        if (dto.Adults != null)
        {
            foreach (var adult in dto.Adults)
            {
                client.Adults.Add(new ClientAdult
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = client.Id,
                    FullName = adult.FullName,
                    Age = adult.Age,
                    Gender = adult.Gender
                });
            }
        }

        await _context.SaveChangesAsync();

        return MapToDto(client);
    }

    public async Task DeleteAsync(string id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ClientDto?> GetByIdAsync(string id)
    {
        var client = await _context.Clients
            .Include(c => c.Addresses)
            .Include(c => c.Childrens)
            .Include(c => c.Adults)
            .FirstOrDefaultAsync(c => c.Id == id);

        return client == null ? null : MapToDto(client);
    }

    public async Task<IEnumerable<ClientDto>> GetAllAsync()
    {
        var clients = await _context.Clients
            .Include(c => c.Addresses)
            .Include(c => c.Childrens)
            .Include(c => c.Adults)
            .ToListAsync();

        return clients.Select(MapToDto);
    }

    private static ClientDto MapToDto(Client client)
    {
        return new ClientDto
        {
            Id = client.Id,
            FullName = client.FullName,
            Street = client.Street,
            Landmark = client.Landmark,
            GeolocationOfClientAddress = client.GeolocationOfClientAddress,
            PhoneNumber = client.PhoneNumber,
            Age = client.Age,
            SocialUsername = client.SocialUsername,
            Language = client.Language,
            HavingChildren = client.HavingChildren,
            HavingElderly = client.HavingElderly,
            Gender = client.Gender,
            FamilyStatus = client.FamilyStatus,
            EconomicStanding = client.EconomicStanding,
            ChildrensAge = client.ChildrensAge,
            Type = client.Type,
            ContactType = client.ContactType,
            TakeIntoAccount = client.TakeIntoAccount,
            DiscountForClient = client.DiscountForClient,
            Addresses = client.Addresses.Select(a => new AddressDto
            {
                Id = a.Id,
                ClientId = a.ClientId,
                Street = a.Street,
                Landmark = a.Landmark,
                City = a.City,
                GeolocationOfClientAddress = a.GeolocationOfClientAddress
            }).ToList(),
            Childrens = client.Childrens.Select(c => new ClientChildrenDto
            {
                Id = c.Id,
                ClientId = c.ClientId,
                FullName = c.FullName,
                Age = c.Age,
                Gender = c.Gender
            }).ToList(),
            Adults = client.Adults.Select(a => new ClientAdultDto
            {
                Id = a.Id,
                ClientId = a.ClientId,
                FullName = a.FullName,
                Age = a.Age,
                Gender = a.Gender
            }).ToList()
        };
    }
}
