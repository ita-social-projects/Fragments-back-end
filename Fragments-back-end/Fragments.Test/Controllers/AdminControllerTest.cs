using FluentAssertions.Execution;
using Fragments.Domain.Dto;
using Fragments.Domain.Services.Interfaces;
using Fragments.Test.Base;
using Fragments.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Fragments.Test.Controllers
{
    public class AdminControllerTest : Base.Base
    {
        private readonly Mock<IAdminService> adminService;
        private readonly AdminController adminController;

        public AdminControllerTest()
        {
            adminService = new Mock<IAdminService>();
            adminController = new AdminController(adminService.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task AssignRole_WhenAdminExists_ReturnsOkResult(RoleDto roleDto, int id)
        {
            // Arrange

            // Act
            var result = await adminController.AssignRole( roleDto, id);

            // Assert
            result.Should().BeOfType<OkResult>(); 
        }
        [Theory]
        [AutoEntityData]
        public async Task getPage_WhenAdminExists_ReturnsOkResult(FilterAndSearchDto filterAndSearchDto,
            SortDto sortDto,
            int page)
        {
            // Arrange

            // Act
            var result = await adminController.getPage(filterAndSearchDto,sortDto,page);

            // Assert
            result.Should().BeOfType<List<AdminDto>>();
        }
        [Fact]
        public async Task GetAll_WhenAdminExists_ReturnsOkObjectResult( )
        {
            // Arrange
          
            // Act
            var result = await adminController.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Theory]
        [AutoEntityData]
        public async Task getUsersBySearch_WhenAdminExists_ReturnsOkObjectResult(FilterAndSearchDto filterAndSearchDto)
        {
            // Arrange

            // Act
            var result = await adminController.getSearchAsync(filterAndSearchDto);

            // Assert
            result.Should().BeNull();
        }
        [Theory]
        [AutoEntityData]
        public async Task Sort_WhenAdminExists_ReturnsOkObjectResult(SortDto sortDto)
        {
            // Arrange

            // Act
            var result = await adminController.Sort(sortDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
