﻿using Paylocity.Employees.Models;
using Paylocity.Employees.Repositories.Interfaces;
using Paylocity.Employees.Services.Interfaces;
using Paylocity.Shared.Models;

namespace Paylocity.Employees.Services
{
    public class DependentService : IDependentService
    {
        private readonly IDependentRepository _dependentRepository;
        public DependentService(IDependentRepository dependentRepository)
        {
            _dependentRepository = dependentRepository;
        }
        public async Task<IEnumerable<Dependent>> GetAll()
        {
            var result = await _dependentRepository.Get();

            return result.ToList();
        }

        public async Task<Dependent?> GetByID(int id)
        {
            return await _dependentRepository.GetByID(id);
        }

        public async Task<CreateObjectResponse> Create(Dependent newDependent)
        {
            return await _dependentRepository.Create(newDependent);
        }
    }
}
