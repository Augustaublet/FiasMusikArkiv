using NSubstitute;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FiasMusikArkiv.Server.Controllers;
using FiasMusikArkiv.Server.Services;
using System.Collections.Generic;
using FiasMusikArkiv.Server.Data.DTOs;

namespace FiasMusikArkiv.Test
{
    public class SongControllerTests
    {
        private readonly SongController _controller;
        private readonly ISongService _songService; // Mocka tjänsten

        public SongControllerTests()
        {
            // Skapa mock för ISongService
            _songService = Substitute.For<ISongService>();

            // Inject mocktjänsten i controllern
            _controller = new SongController(_songService);
        }

        [Fact]
        public async Task GetAllSongs_ReturnsOkResult_WithListOfSongs()
        {
            // Arrange
            var mockSongs = new List<SongDto>
        {
            new SongDto { Id = 1, Name = "Song 1", Description = "Description 1", GenreId= 1},
            new SongDto { Id = 1, Name = "Song 2", Description = "Description 2", GenreId= 1 }
        };

            // Mock tjänsten att returnera en lista av sånger
            _songService.GetAllSongsAsync().Returns(mockSongs);

            // Act
            var result = await _controller.GetAllSongs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Kolla att vi får en Ok-result
            var returnSongs = Assert.IsType<List<SongDto>>(okResult.Value); // Kolla att vi får rätt typ tillbaka
            Assert.Equal(2, returnSongs.Count); // Kolla att antalet sånger stämmer
        }

        [Fact]
        public async Task GetAllSongs_ReturnsNotFound_WhenNoSongsExist()
        {
            // Arrange
            _songService.GetAllSongsAsync().Returns((List<SongDto>)null); // Mock tjänsten att returnera null

            // Act
            var result = await _controller.GetAllSongs();

            // Assert
            Assert.IsType<NotFoundResult>(result); // Kolla att vi får en NotFound-result
        }
    }
}