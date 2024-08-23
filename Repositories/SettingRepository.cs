using Karma.MVC.Data;

namespace Karma.MVC.Repositories;

public class SettingRepository
{
	private readonly Dictionary<string, string> _keyValues;

	public SettingRepository(AppDbContext context)
	{
		_keyValues = context.Settings.ToDictionary(n => n.Key, n => n.Value);
	}

	public string Get(string key)
	{
		var data = _keyValues[key];

		return data;
	}

	public Dictionary<string, string> GetAll()
	{
		var data = _keyValues;

		return data;
	}
}
