using Castle.Core.Configuration;
using FluentAssertions.Execution;
using Fragments.Data.Context;
using Fragments.Data.Entities;
using Fragments.Domain.Dto;
using Fragments.Domain.Services.Implementation;
using Fragments.Domain.Services.Interfaces;
using Fragments.Test.Base;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Fragments.Test.Services
{
    public class AdminServiceTest : Base.Base 
    {
        private readonly FragmentsContext context;
        private readonly Mock<IConfiguration> configuration;
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;
        private readonly IAdminService service;
        public AdminServiceTest()
        {
            configuration = new Mock<IConfiguration>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            context = ContextGenerator.GetContext();
            service = new AdminService(context, Mapper);
        }

        [Fact]
        public async Task GetUsersAsync_WhenAdminExist_ReturnsList()
        {
            //Arrange

            //Act
            var result = await service.GetUsersAsync();

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }

        [Fact]
        public async Task Sort_WhenSortDtoIsNull_ReturnsList()
        {
            //Arrange
            SortDto? sortDto = null;

            //Act
            var result = await service.Sort(sortDto!);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task Sort_SortingByFullNameAsc_ReturnsList()
        {
            //Arrange
            SortDto sortDto  = new SortDto();
            sortDto.IsAscending = true;
            sortDto.PropertyName = "FullName";

            //Act
            var result = await service.Sort(sortDto);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task Sort_SortingByFullNameDesc_ReturnsList()
        {
            //Arrange
            SortDto sortDto = new SortDto();
            sortDto.IsAscending = false;
            sortDto.PropertyName = "FullName";

            //Act
            var result = await service.Sort(sortDto);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task Sort_SortingByEmailAsc_ReturnsList()
        {
            //Arrange
            SortDto sortDto = new SortDto();
            sortDto.IsAscending = true;
            sortDto.PropertyName = "Email";

            //Act
            var result = await service.Sort(sortDto);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task Sort_SortingByEmailDesc_ReturnsList()
        {
            //Arrange
            SortDto sortDto = new SortDto();
            sortDto.IsAscending = false;
            sortDto.PropertyName = "Email";

            //Act
            var result = await service.Sort(sortDto);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task Sort_SortingByDefaultCase_ReturnsList()
        {
            //Arrange
            SortDto sortDto = new SortDto();
            sortDto.IsAscending = true;
            sortDto.PropertyName = "Default";

            //Act
            var result = await service.Sort(sortDto);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task GetUserWithSearchAsync_WhenAdminExist_ReturnsList()
        {
            //Arrange
            FilterAndSearchDto filterAndSearchDto = new FilterAndSearchDto();

            //Act
            var result = await service.GetUserWithSearchAsync(filterAndSearchDto);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task GetPage_WhenSortDtoIsNull_ReturnsList()
        {
            //Arrange
            FilterAndSearchDto filterAndSearchDto = new FilterAndSearchDto();
            SortDto? sortDto = null;
            int page= 1;

            //Act
            var result = await service.GetPageAsync(sortDto!, filterAndSearchDto,page);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task GetPage_WhenSortingByNameAsc_ReturnsList()
        {
            //Arrange
            FilterAndSearchDto filterAndSearchDto = new FilterAndSearchDto();
            SortDto? sortDto = new SortDto();
            sortDto.IsAscending = true;
            sortDto.PropertyName = "FullName";
            int page = 1;

            //Act
            var result = await service.GetPageAsync(sortDto!, filterAndSearchDto, page);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task GetPage_WhenSortingByNameDesc_ReturnsList()
        {
            //Arrange
            FilterAndSearchDto filterAndSearchDto = new FilterAndSearchDto();
            SortDto? sortDto = new SortDto();
            sortDto.IsAscending = false;
            sortDto.PropertyName = "FullName";
            int page = 1;

            //Act
            var result = await service.GetPageAsync(sortDto!, filterAndSearchDto, page);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task GetPage_WhenSortingByEmailAsc_ReturnsList()
        {
            //Arrange
            FilterAndSearchDto filterAndSearchDto = new FilterAndSearchDto();
            SortDto? sortDto = new SortDto();
            sortDto.IsAscending = true;
            sortDto.PropertyName = "Email";
            int page = 1;

            //Act
            var result = await service.GetPageAsync(sortDto!, filterAndSearchDto, page);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task GetPage_WhenSortingByEmailDesc_ReturnsList()
        {
            //Arrange
            FilterAndSearchDto filterAndSearchDto = new FilterAndSearchDto();
            SortDto? sortDto = new SortDto();
            sortDto.IsAscending = false;
            sortDto.PropertyName = "Email";
            int page = 1;

            //Act
            var result = await service.GetPageAsync(sortDto!, filterAndSearchDto, page);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task GetPage_WhenSortingByDefault_ReturnsList()
        {
            //Arrange
            FilterAndSearchDto filterAndSearchDto = new FilterAndSearchDto();
            SortDto? sortDto = new SortDto();
            sortDto.IsAscending = false;
            sortDto.PropertyName = "Default";
            int page = 1;

            //Act
            var result = await service.GetPageAsync(sortDto!, filterAndSearchDto, page);

            //Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
    }
}
