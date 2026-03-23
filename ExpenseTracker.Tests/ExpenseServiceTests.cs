using AutoMapper;
using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Application.Mappings;
using ExpenseTracker.Application.Services;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;
using FluentAssertions;
using Moq;

namespace ExpenseTracker.Tests.Services
{
    public class ExpenseServiceTests
    {
        private readonly Mock<IExpenseRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly ExpenseService _service;

        public ExpenseServiceTests()
        {
            _mockRepo = new Mock<IExpenseRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();

            _service = new ExpenseService(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async Task GetAllExpenses_ShouldReturnExpenseList()
        {
            // Arrange
            var expenses = new List<Expense>
            {
                new Expense
                {
                    Id = 1,
                    Amount = 100,
                    Description = "Food",
                    Date = DateTime.UtcNow,
                    Category = new Category { Name = "Food" }
                }
            };

            _mockRepo.Setup(x => x.GetAllAsync())
                     .ReturnsAsync(expenses);

            // Act
            var result = await _service.GetAllExpenses();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().CategoryName.Should().Be("Food");
        }
        [Fact]
        public async Task GetExpenseById_ShouldReturnExpense_WhenExists()
        {
            // Arrange
            var expense = new Expense
            {
                Id = 1,
                Amount = 200,
                Description = "Travel",
                Date = DateTime.UtcNow,
                Category = new Category { Name = "Travel" }
            };

            _mockRepo.Setup(x => x.GetByIdAsync(1))
                     .ReturnsAsync(expense);

            // Act
            var result = await _service.GetExpenseById(1);

            // Assert
            result.Should().NotBeNull();
            result!.Amount.Should().Be(200);
        }
        [Fact]
        public async Task AddExpense_ShouldCallRepository()
        {
            // Arrange
            var dto = new CreateExpenseDto
            {
                Amount = 150,
                Description = "Snacks",
                CategoryId = 1,
                UserId = 1
            };

            // Act
            await _service.AddExpense(dto);

            // Assert
            _mockRepo.Verify(x => x.AddAsync(It.IsAny<Expense>()), Times.Once);
        }
        [Fact]
        public async Task DeleteExpense_ShouldThrowException_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(x => x.GetByIdAsync(1))
                     .ReturnsAsync((Expense)null);

            // Act
            Func<Task> act = async () => await _service.DeleteExpense(1);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                     .WithMessage("Expense not found");
        }
    }
}