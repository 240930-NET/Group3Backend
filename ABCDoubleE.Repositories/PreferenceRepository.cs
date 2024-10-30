using Microsoft.EntityFrameworkCore;
using ABCDoubleE.Models;
using ABCDoubleE.Data;

public class PreferenceRepository : IPreferenceRepository
{
    private readonly ABCDoubleEContext _context;

    public PreferenceRepository(ABCDoubleEContext context)
    {
        _context = context;
    }

    public async Task<Preference?> GetPreferenceByUserIdAsync(int userId)
    {
        return await _context.Preferences
            .Include(p => p.favBooks)
            .FirstOrDefaultAsync(p => p.userId == userId);
    }

    public async Task<Preference?> GetPreferenceByIdAsync(int preferenceId)
    {
        return await _context.Preferences
            .Include(p => p.favBooks)
            .FirstOrDefaultAsync(p => p.preferenceId == preferenceId);
    }

    public async Task AddPreferenceAsync(Preference preference)
    {
        await _context.Preferences.AddAsync(preference);
        await _context.SaveChangesAsync();
    }


    public async Task UpdatePreferenceAsync(Preference preference)
    {
        _context.Preferences.Update(preference);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePreferenceAsync(int preferenceId)
    {
        var preference = await _context.Preferences.FindAsync(preferenceId);
        if (preference != null)
        {
            _context.Preferences.Remove(preference);
            await _context.SaveChangesAsync();
        }
    }
}
