using Microsoft.AspNetCore.Components;
using MudBlazor;
using RPGClient.Models.Character;
using RPGClient.Models.Skill;
using RPGClient.Services.Contracts;

namespace RPGClient.Components.Skills;

public partial class CharacterSkills
{
    [Parameter] public string CharacterName { get; set; } = null!;
    [Parameter] public int CharacterId { get; set; }
    [Parameter] public List<GetSkillDto>? OwnedSkills { get; set; }
    [Inject] private ISkillService SkillService { get; set; } = null!;
    [Inject] private ICharacterService CharacterService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    private List<GetSkillDto> _allSkills = new();
    private bool _loadingAllSkills = true;
    private bool _loadingCharacterSkills = false;

    protected override async Task OnInitializedAsync()
    {
        await GetSkillsFromServer();
    }

    private async Task GetSkillsFromServer()
    {
        try
        {
            _allSkills = await SkillService.GetAll();
            _loadingAllSkills = false;
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
    }

    private async Task AddCharacterSkill(int skillId)
    {
        _loadingCharacterSkills = true;

        var characterSkill = new CharacterSkillDto() { CharacterId = CharacterId, SkillId = skillId };
        try
        {
            OwnedSkills = await CharacterService.AddCharacterSkill(characterSkill);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }

        _loadingCharacterSkills = false;
    }
    
    private async Task RemoveCharacterSkill(int skillId)
    {
        _loadingCharacterSkills = true;

        var characterSkill = new CharacterSkillDto() { CharacterId = CharacterId, SkillId = skillId };
        try
        {
            OwnedSkills = await CharacterService.RemoveCharacterSkill(characterSkill);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }

        _loadingCharacterSkills = false;
    }
}