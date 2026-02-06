using Microsoft.EntityFrameworkCore;
using YallaPharm.Application.DTOs;
using YallaPharm.Application.Services.Interfaces;
using YallaPharm.Domain.Entities;
using YallaPharm.Infrastructure.Data;

namespace YallaPharm.Application.Services;

public class PharmacyService : IPharmacyService
{
    private readonly ApplicationDbContext _context;

    public PharmacyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PharmacyDto> CreateAsync(PharmacyDto dto)
    {
        var pharmacy = new Pharmacy
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Address = dto.Address,
            Landmark = dto.Landmark,
            Contact = dto.Contact,
            GeolocationLink = dto.GeolocationLink,
            Country = dto.Country,
            Markup = dto.Markup,
            MarkupType = dto.MarkupType,
            IsRequired = dto.IsRequired,
            IsAbroad = dto.IsAbroad,
            PayoutMethod = dto.PayoutMethod
        };

        if (dto.PharmacyContacts != null)
        {
            foreach (var contact in dto.PharmacyContacts)
            {
                pharmacy.PharmacyContacts.Add(new PharmacyContact
                {
                    Id = Guid.NewGuid().ToString(),
                    PharmacyId = pharmacy.Id,
                    Contact = contact.Contact,
                    ContactType = contact.ContactType
                });
            }
        }

        _context.Pharmacies.Add(pharmacy);
        await _context.SaveChangesAsync();

        return MapToDto(pharmacy);
    }

    public async Task<PharmacyDto> UpdateAsync(PharmacyDto dto)
    {
        var pharmacy = await _context.Pharmacies
            .Include(p => p.PharmacyContacts)
            .FirstOrDefaultAsync(p => p.Id == dto.Id);

        if (pharmacy == null)
            throw new KeyNotFoundException($"Pharmacy with ID {dto.Id} not found");

        pharmacy.Name = dto.Name;
        pharmacy.Address = dto.Address;
        pharmacy.Landmark = dto.Landmark;
        pharmacy.Contact = dto.Contact;
        pharmacy.GeolocationLink = dto.GeolocationLink;
        pharmacy.Country = dto.Country;
        pharmacy.Markup = dto.Markup;
        pharmacy.MarkupType = dto.MarkupType;
        pharmacy.IsRequired = dto.IsRequired;
        pharmacy.IsAbroad = dto.IsAbroad;
        pharmacy.PayoutMethod = dto.PayoutMethod;

        _context.PharmacyContacts.RemoveRange(pharmacy.PharmacyContacts);
        if (dto.PharmacyContacts != null)
        {
            foreach (var contact in dto.PharmacyContacts)
            {
                pharmacy.PharmacyContacts.Add(new PharmacyContact
                {
                    Id = Guid.NewGuid().ToString(),
                    PharmacyId = pharmacy.Id,
                    Contact = contact.Contact,
                    ContactType = contact.ContactType
                });
            }
        }

        await _context.SaveChangesAsync();

        return MapToDto(pharmacy);
    }

    public async Task DeleteAsync(string id)
    {
        var pharmacy = await _context.Pharmacies.FindAsync(id);
        if (pharmacy != null)
        {
            _context.Pharmacies.Remove(pharmacy);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<PharmacyDto?> GetByIdAsync(string id)
    {
        var pharmacy = await _context.Pharmacies
            .Include(p => p.PharmacyContacts)
            .FirstOrDefaultAsync(p => p.Id == id);

        return pharmacy == null ? null : MapToDto(pharmacy);
    }

    public async Task<IEnumerable<PharmacyDto>> GetAllAsync()
    {
        var pharmacies = await _context.Pharmacies
            .Include(p => p.PharmacyContacts)
            .ToListAsync();

        return pharmacies.Select(MapToDto);
    }

    private static PharmacyDto MapToDto(Pharmacy pharmacy)
    {
        return new PharmacyDto
        {
            Id = pharmacy.Id,
            Name = pharmacy.Name,
            Address = pharmacy.Address,
            Landmark = pharmacy.Landmark,
            Contact = pharmacy.Contact,
            GeolocationLink = pharmacy.GeolocationLink,
            Country = pharmacy.Country,
            Markup = pharmacy.Markup,
            MarkupType = pharmacy.MarkupType,
            IsRequired = pharmacy.IsRequired,
            IsAbroad = pharmacy.IsAbroad,
            PayoutMethod = pharmacy.PayoutMethod,
            PharmacyContacts = pharmacy.PharmacyContacts.Select(c => new PharmacyContactDto
            {
                Id = c.Id,
                PharmacyId = c.PharmacyId,
                Contact = c.Contact,
                ContactType = c.ContactType
            }).ToList()
        };
    }
}
