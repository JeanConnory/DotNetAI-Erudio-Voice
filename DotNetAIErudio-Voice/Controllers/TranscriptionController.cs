﻿using DotNetAIErudio_Voice.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAIErudio_Voice.Controllers;

[ApiController]
[Route("ai")]
public class TranscriptionController : ControllerBase
{
    private readonly TranscriptionService _transcriptionService;

    public TranscriptionController(TranscriptionService transcriptionService)
    {
        _transcriptionService = transcriptionService;
    }

    [HttpPost("transcribe")]
    public async Task<IActionResult> TranscribeAudio([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded or file is empty!");

        try
        {
            var transcription = await _transcriptionService.TranscribeAudioAsync(file);
            return Ok(transcription);
        }
        catch
        {
            return StatusCode(500, "An error ocurred while processing your audio file");
        }
    }
}
