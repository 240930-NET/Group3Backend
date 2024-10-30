namespace ABCDoubleE.Services;

using ABCDoubleE.DTOs;
using ABCDoubleE.Models;

public class PreferenceService : IPreferenceService
{
    private readonly IPreferenceRepository _preferenceRepository;

    public PreferenceService(IPreferenceRepository preferenceRepository)
    {
        _preferenceRepository = preferenceRepository;
    }

    // Get Preference by User ID
    public async Task<PreferenceDTO?> GetPreferenceByUserIdAsync(int userId)
    {
        var preference = await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
        if (preference == null)
        {
            return null;
        }

        return new PreferenceDTO
        {
            preferenceId = preference.preferenceId,
            favGenres = preference.favGenres,
            favBookIds = preference.favBooks.Select(b => b.bookId).ToList(),
            favAuthors = preference.favAuthors
        };
    }

    // Get Preference by Preference ID
    public async Task<PreferenceDTO?> GetPreferenceByIdAsync(int preferenceId)
    {
        var preference = await _preferenceRepository.GetPreferenceByIdAsync(preferenceId);
        if (preference == null)
        {
            return null;
        }

        return new PreferenceDTO
        {
            preferenceId = preference.preferenceId,
            favGenres = preference.favGenres,
            favBookIds = preference.favBooks.Select(b => b.bookId).ToList(),
            favAuthors = preference.favAuthors
        };
    }

    // Create new Preference for a User
    public async Task<PreferenceDTO> CreatePreferenceAsync(int userId, PreferenceCreateDTO preferenceCreateDto)
    {
        var preference = new Preference
        {
            userId = userId,
            favGenres = preferenceCreateDto.favGenres,
            favAuthors = preferenceCreateDto.favAuthors,
            favBooks = preferenceCreateDto.favBookIds.Select(bookId => new Book { bookId = bookId }).ToList()
        };

        await _preferenceRepository.AddPreferenceAsync(preference);

        return new PreferenceDTO
        {
            preferenceId = preference.preferenceId,
            favGenres = preference.favGenres,
            favBookIds = preference.favBooks.Select(b => b.bookId).ToList(),
            favAuthors = preference.favAuthors
        };
    }

    // Update existing Preference
    public async Task<PreferenceDTO?> UpdatePreferenceAsync(int userId, PreferenceUpdateDTO preferenceUpdateDto)
    {
        var preference = await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
        if (preference == null)
        {
            return null;
        }

        preference.favGenres = preferenceUpdateDto.favGenres;
        preference.favAuthors = preferenceUpdateDto.favAuthors;
        preference.favBooks = preferenceUpdateDto.favBookIds.Select(bookId => new Book { bookId = bookId }).ToList();

        await _preferenceRepository.UpdatePreferenceAsync(preference);

        return new PreferenceDTO
        {
            preferenceId = preference.preferenceId,
            favGenres = preference.favGenres,
            favBookIds = preference.favBooks.Select(b => b.bookId).ToList(),
            favAuthors = preference.favAuthors
        };
    }
}
