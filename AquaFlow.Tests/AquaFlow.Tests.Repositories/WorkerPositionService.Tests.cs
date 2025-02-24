using AquaFlow.DataAccess.Models;
using AquaFlow.DataAccess.Interfaces;
using AquaFlow.Domain.DTOs.WorkerPosition;
using AquaFlow.Domain.Services;
using Moq;
using AutoMapper;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class WorkerPositionServiceTests
{
    private readonly Mock<IWorkerPositionRepository> _mockWorkerPositionRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly WorkerPositionService _workerPositionService;

    public WorkerPositionServiceTests()
    {
        _mockWorkerPositionRepository = new Mock<IWorkerPositionRepository>();
        _mockMapper = new Mock<IMapper>();
        _workerPositionService = new WorkerPositionService(_mockWorkerPositionRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetWorkerPositionsAsync_ReturnsWorkerPositions()
    {
        var workerPositions = new List<WorkerPosition>
        {
            new WorkerPosition { Id = 1, PositionName = "Position 1" },
            new WorkerPosition { Id = 2, PositionName = "Position 2" }
        };

        var workerPositionDTOs = new List<RetrieveWorkerPositionDTO>
        {
            new RetrieveWorkerPositionDTO { Id = 1, PositionName = "Position 1" },
            new RetrieveWorkerPositionDTO { Id = 2, PositionName = "Position 2" }
        };

        _mockWorkerPositionRepository.Setup(r => r.GetWorkerPositionsAsync())
            .ReturnsAsync(workerPositions);

        _mockMapper.Setup(m => m.Map<IEnumerable<RetrieveWorkerPositionDTO>>(workerPositions))
            .Returns(workerPositionDTOs);

        var result = await _workerPositionService.GetWorkerPositionsAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, dto => dto.PositionName == "Position 1");
        Assert.Contains(result, dto => dto.PositionName == "Position 2");
    }

    [Fact]
    public async Task GetWorkerPositionsAsync_ThrowsException_WhenRepositoryFails()
    {
        _mockWorkerPositionRepository.Setup(r => r.GetWorkerPositionsAsync())
            .ThrowsAsync(new Exception("Repository error"));

        var exception = await Assert.ThrowsAsync<Exception>(() => _workerPositionService.GetWorkerPositionsAsync());
        Assert.Equal("SVC: Error while fetching worker positions.", exception.Message);
    }

    [Fact]
    public async Task GetWorkerPositionsAsync_ThrowsException_WhenMapperFails()
    {
        var workerPositions = new List<WorkerPosition>
        {
            new WorkerPosition { Id = 1, PositionName = "Position 1" }
        };

        _mockWorkerPositionRepository.Setup(r => r.GetWorkerPositionsAsync())
            .ReturnsAsync(workerPositions);

        _mockMapper.Setup(m => m.Map<IEnumerable<RetrieveWorkerPositionDTO>>(workerPositions))
            .Throws(new Exception("Mapper error"));

        var exception = await Assert.ThrowsAsync<Exception>(() => _workerPositionService.GetWorkerPositionsAsync());
        Assert.Equal("SVC: Error while fetching worker positions.", exception.Message);
    }
}
