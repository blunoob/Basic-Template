using UnityEngine;

public class SeparatorAttribute : PropertyAttribute {
	public readonly string title;
	public readonly float textGap;
	public readonly float borderOffset;

	public SeparatorAttribute()
		: this("") {
	}

	public SeparatorAttribute(string _title, float textGap = 10.0f, float borderOffset = 0.0f) {
		this.title = _title;
		this.textGap = textGap;
		this.borderOffset = borderOffset;
	}
}