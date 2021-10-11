﻿namespace UpSkill.Services.Data.Company 
{
    using System.Threading.Tasks; 

    using Microsoft.EntityFrameworkCore;

    using UpSkill.Common;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Services.Data.Contracts.Company;
    using UpSkill.Web.ViewModels.Company;

    using static Common.GlobalConstants.CompaniesConstants;

    public class CompaniesService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companies;

        public CompaniesService(IDeletableEntityRepository<Company> companies) 
            => this.companies = companies;

        public async Task<Result> CreateAsync(CreateCompanyRequestModel model)
        {
            var companyObj = await this.companies
                 .AllAsNoTracking()
                 .FirstOrDefaultAsync(c => c.Name == model.Name);

            if (companyObj != null)
            {
                return AlreadyExist;
            }

            var company = new Company()
            {
                Name = model.Name
            };

            await this.companies.AddAsync(company);

            await this.companies.SaveChangesAsync();

            return true;
        }

        public async Task<Result> EditAsync(UpdateCompanyRequestModel model)
        {
            var company = await this.companies
             .All()
             .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (company == null)
            {
                return DoesNotExist;
            }

            company.Name = model.Name;

            await this.companies.SaveChangesAsync();

            return true;
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var company = await this.companies 
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (company == null)
            {
                return DoesNotExist;
            }

            this.companies.Delete(company);

            await this.companies.SaveChangesAsync();

            return true;
        }

        public async Task<ReturnCompanyResponseModel> GetCompanyByIdAsync(int id)
        {
            var company = await this.companies
                .AllAsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            var model = new ReturnCompanyResponseModel() { Company = company };

            return model;
        }  
    }
}
