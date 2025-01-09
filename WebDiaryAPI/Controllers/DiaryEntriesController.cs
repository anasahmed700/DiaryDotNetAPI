﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDiaryAPI.Data;
using WebDiaryAPI.Models;

namespace WebDiaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryEntriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiaryEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaryEntry>>> GetDiaryEntries()
        {
            return await _context.DiaryEntries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiaryEntry>> GetDiaryEntry(int id)
        {
            var diaryEntry = await _context.DiaryEntries.FindAsync(id);
            if (diaryEntry == null)
            {
                return NotFound();
            }

            return diaryEntry;
        }

        [HttpPost]
        public async Task<ActionResult<DiaryEntry>> CreateDiaryEntry(DiaryEntry diaryEntry)
        {
            diaryEntry.Id = 0;
            _context.DiaryEntries.Add(diaryEntry);
            await _context.SaveChangesAsync();
            var resourceUrl = Url.Action(nameof(GetDiaryEntry), new { id = diaryEntry.Id });

            return Created(resourceUrl, diaryEntry);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDiaryEntry(int id, [FromBody] DiaryEntry diaryEntry)
        {
            if (id != diaryEntry.Id)
            {
                return BadRequest();
            }
            _context.Entry(diaryEntry).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!DiaryEntryExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveDiaryEntry(int id)
        {
            var diaryEntry = await _context.DiaryEntries.FindAsync(id);
            if (diaryEntry == null)
            {
                return NotFound();
            }

            _context.DiaryEntries.Remove(diaryEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiaryEntryExists(int id)
        {
            return _context.DiaryEntries.Any(e => e.Id == id);
        }
    }
}
