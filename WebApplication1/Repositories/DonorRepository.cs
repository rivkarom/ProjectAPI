using ChineseAuctionProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using StoreApi.Data;
using static ChineseAuctionProject.DTOs.DonorDTOs;

namespace ChineseAuctionProject.Repositories
{
    public class DonorRepository:IDonorRepository
    {
        private readonly ApplicationDbContext _context;

        public DonorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DonorResponseDto>> GetAllDonorsAsync()
        {
            return await _context.Donors
                .Select(donor => new DonorResponseDto
                {
                    Id = donor.Id,
                    Name = donor.Name,
                    Email = donor.Email,
                    Phone = donor.Phone
                })
                .ToListAsync();
        }
        public async Task<DonorResponseDto?> GetDonorByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null)
            {
                return null;
            }
            return new DonorResponseDto
            {
                Id = donor.Id,
                Name = donor.Name,
                Email = donor.Email,
                Phone = donor.Phone
            };
        }
        public async Task<DonorResponseDto> CreateDonorAsync(DonorCreateDto createDto)
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }
            var donor = new Models.Donor
            {
                Id = createDto.Id,
                Name = createDto.Name,
                Email = createDto.Email,
                Phone = createDto.Phone
            };
            _context.Donors.Add(donor);
            await _context.SaveChangesAsync();
            return new DonorResponseDto
            {
                Id = donor.Id,
                Name = donor.Name,
                Email = donor.Email,
                Phone = donor.Phone
            };
        }
        public async Task<DonorResponseDto?> UpdateDonorAsync(int id, DonorUpdateDto updateDto)
        {
            if (updateDto == null || id <= 0)
            {
                return null;
            }
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null)
            {
                throw new ArgumentException("Donor not found", nameof(id));
            }
            donor.Name = updateDto.Name ?? donor.Name;
            donor.Email = updateDto.Email ?? donor.Email;
            donor.Phone = updateDto.Phone ?? donor.Phone;
            _context.Donors.Update(donor);

            await _context.SaveChangesAsync();
            return new DonorResponseDto
            {
                Id = donor.Id,
                Name = donor.Name,
                Email = donor.Email,
                Phone = donor.Phone
            };
        }
        //public async Task<LoginResponseDto?> AuthenticateAsync(string email, string password);
    }
}
