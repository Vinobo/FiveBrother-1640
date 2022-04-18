﻿using FiveBrother.Data;
using FiveBrother.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveBrother.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["DepartmentIds"] = new SelectList(_context.Departments, "Id", "Name", user.DepartmentId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, CUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id)) return NotFound();
                    else throw;

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentIds"] = new SelectList(_context.Categories, "Id", "Code", user.DepartmentId);
            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Classrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AssignRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            var currentRoleId = await _context.UserRoles.Where(u => u.UserId == user.Id).Select(u => u.RoleId).ToListAsync();
            var remainingRoleIds = await _context.Roles.Where(u => !currentRoleId.Contains(u.Id)).Select(u => u.Id).ToListAsync();

            ViewData["RemaniningRoleIds"] = remainingRoleIds;
            ViewData["CurrentRoleIds"] = currentRoleId;

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string roleId, string userId)
        {
            if (roleId != null && userId != null)
            {
                var userrole = new IdentityUserRole<string>()
                {
                    UserId = userId,
                    RoleId = roleId
                };
                _context.Add(userrole);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AssignRole), new { id = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRole(string roleId, string userId)
        {
            var userrole = await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == userId && u.RoleId == roleId);

            if (userrole != null)
            {
                _context.Remove(userrole);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AssignRole), new { id = userId });
        }
    }
}
