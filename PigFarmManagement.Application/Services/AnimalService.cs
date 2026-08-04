using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PigFarmManagement.Application.DTOs;
using PigFarmManagement.Application.Helpers;
using PigFarmManagement.Application.Interfaces.Repositories;
using PigFarmManagement.Application.Interfaces.Services;
using PigFarmManagement.Application.Mappings;
using PigFarmManagement.Domain.Entities;
using PigFarmManagement.Domain.Enums;
using static PigFarmManagement.Application.DTOs.AnimalModels;

namespace PigFarmManagement.Application.Services
{
    public class AnimalService(
        IAnimalRepository animalRepo,
        ICurrentUserServices currentUser,
        IFarmRepository farmRepo)
        : IAnimalService
    {
        private readonly IAnimalRepository _animalRepo = animalRepo;
        private readonly ICurrentUserServices _currentUser = currentUser;
        private readonly IFarmRepository _farmRepo = farmRepo;

        public async Task<AnimalResponse> AddAsync(CreateAnimalRequest createAnimalRequest, CancellationToken cancellationToken)
        {
            var farmId = _currentUser.FarmId;
            var farm = await _farmRepo.GetByIdAsync(
                farmId,
                cancellationToken)
                ?? throw new InvalidOperationException("Farm not found.");

            if (createAnimalRequest.SowId.HasValue)
            {
                var sow = await _animalRepo.GetByIdAsync(
                    createAnimalRequest.SowId.Value,
                    cancellationToken);

                if (sow == null || sow.Gender != Gender.Female)
                    throw new Exception("Invalid sow.");
            }

            if (createAnimalRequest.BoarId.HasValue)
            {
                var boar = await _animalRepo.GetByIdAsync(createAnimalRequest.BoarId.Value, cancellationToken);
                if (boar == null || boar.Gender != Gender.Male)
                    throw new Exception("Invalid boar.");
            }

            farm.LastAnimalSequence++;

            var tagNumber = TagNumberGenerator.Generate(
                farm.FarmCode,
                createAnimalRequest.DateOfBirth,
                farm.LastAnimalSequence);

            var animal = new Animal
            {
                TagNumber = tagNumber,
                DateOfBirth = createAnimalRequest.DateOfBirth,
                BirthWeight = createAnimalRequest.BirthWeight,
                CurrentWeight = createAnimalRequest.BirthWeight,
                Gender = createAnimalRequest.Gender,
                Breed = createAnimalRequest.Breed,
                SowId = createAnimalRequest.SowId,
                BoarId = createAnimalRequest.BoarId,
                BatchId = createAnimalRequest.BatchId,
                Status = AnimalStatus.Alive,
                ProductionStage = PigLifecycleCalculator.Calculate(createAnimalRequest.DateOfBirth),
                Notes = createAnimalRequest.Notes,
                FarmId = farmId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _currentUser.UserId.ToString()
            };

            _farmRepo.Update(farm, cancellationToken);
            await _farmRepo.SaveChangesAsync(cancellationToken);
            await _animalRepo.AddAsync(animal, cancellationToken);
            await _animalRepo.SaveChangesAsync(cancellationToken);
            return AnimalMapper.ToResponse(animal);
        }

        public async Task<AnimalResponse> UpdateAsync(Guid id, UpdateAnimalRequest request, CancellationToken cancellationToken)
        {
            var animal = await _animalRepo.GetByIdAsync(id, cancellationToken);
            if (animal == null || animal.IsDeleted)
                throw new Exception("Animal not found.");

            animal.TagNumber = request.TagNumber;
            animal.DateOfBirth = request.DateOfBirth;
            animal.BirthWeight = request.BirthWeight;
            animal.CurrentWeight = request.CurrentWeight;
            animal.Gender = request.Gender;
            animal.Breed = request.Breed;
            animal.SowId = request.SowId;
            animal.BoarId = request.BoarId;
            animal.BatchId = request.BatchId;
            animal.Status = request.Status;
            animal.ProductionStage = request.ProductionStage;
            animal.Notes = request.Notes;
            animal.UpdatedAt = DateTime.UtcNow;
            animal.UpdatedBy = _currentUser.UserId.ToString();

            _animalRepo.Update(animal, cancellationToken);
            await _animalRepo.SaveChangesAsync(cancellationToken);
            return AnimalMapper.ToResponse(animal);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var animal = await _animalRepo.GetByIdAsync(id, cancellationToken);
            if (animal == null || animal.IsDeleted)
                throw new Exception("Animal not found.");

            _animalRepo.Remove(animal);
            await _animalRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task<AnimalResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var animal = await _animalRepo.GetByIdAsync(id, cancellationToken);
            if (animal == null || animal.IsDeleted)
                return null;

            return AnimalMapper.ToResponse(animal);
        }

        public async Task<AnimalResponse?> GetByTagNumberAsync(string tagNumber, CancellationToken cancellationToken)
        {
            var animal = await _animalRepo.GetByTagNumberAsync(tagNumber, cancellationToken);
            if (animal == null || animal.IsDeleted)
                return null;

            return AnimalMapper.ToResponse(animal);
        }

        public async Task<IReadOnlyList<AnimalSummary>> GetAllAsync(CancellationToken cancellationToken)
        {
            var animals = await _animalRepo.GetAllAsync(cancellationToken);
            return AnimalMapper.ToSummaryList(animals.Where(a => !a.IsDeleted));
        }

        public async Task UpdateStatusAsync(Guid id, AnimalStatus status, CancellationToken cancellationToken)
        {
            var animal = await _animalRepo.GetByIdAsync(id, cancellationToken);
            if (animal == null || animal.IsDeleted)
                throw new Exception("Animal not found.");

            animal.Status = status;
            animal.UpdatedAt = DateTime.UtcNow;
            animal.UpdatedBy = _currentUser.UserId.ToString();

            _animalRepo.Update(animal, cancellationToken);
            await _animalRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateProductionStageAsync(Guid id, ProductionStage stage, CancellationToken cancellationToken)
        {
            var animal = await _animalRepo.GetByIdAsync(id, cancellationToken);
            if (animal == null || animal.IsDeleted)
                throw new Exception("Animal not found.");

            animal.ProductionStage = stage;
            animal.UpdatedAt = DateTime.UtcNow;
            animal.UpdatedBy = _currentUser.UserId.ToString();

            _animalRepo.Update(animal, cancellationToken);
            await _animalRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<AnimalSummary>> GetByStatusAsync(AnimalStatus status, CancellationToken cancellationToken)
        {
            var animals = await _animalRepo.GetAllAsync(cancellationToken);
            return AnimalMapper.ToSummaryList(animals.Where(a => !a.IsDeleted && a.Status == status));
        }

        public async Task<IReadOnlyList<AnimalSummary>> GetByProductionStageAsync(ProductionStage stage, CancellationToken cancellationToken)
        {
            var animals = await _animalRepo.GetAllAsync(cancellationToken);
            return AnimalMapper.ToSummaryList(animals.Where(a => !a.IsDeleted && a.ProductionStage == stage));
        }

        public async Task<IReadOnlyList<AnimalSummary>> GetByGenderAsync(Gender gender, CancellationToken cancellationToken)
        {
            var animals = await _animalRepo.GetAllAsync(cancellationToken);
            return AnimalMapper.ToSummaryList(animals.Where(a => !a.IsDeleted && a.Gender == gender));
        }

        public async Task<IReadOnlyList<AnimalSummary>> GetByBatchAsync(Guid batchId, CancellationToken cancellationToken)
        {
            var animals = await _animalRepo.GetAllAsync(cancellationToken);
            return AnimalMapper.ToSummaryList(animals.Where(a => !a.IsDeleted && a.BatchId == batchId));
        }
    }
}