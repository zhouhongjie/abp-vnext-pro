﻿using CompanyName.ProjectName.DataDictionaryManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CompanyName.ProjectName.DataDictionaryManagement
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(DataDictionaryManagementEntityFrameworkCoreTestModule)
        )]
    public class DataDictionaryManagementDomainTestModule : AbpModule
    {
        
    }
}
