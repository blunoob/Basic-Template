using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(SeparatorAttribute))]
public class SeparatorDrawer : DecoratorDrawer {

	private SeparatorAttribute _separatorAttribute = null;
	private SeparatorAttribute separatorAttribute {
		get {
			if ( _separatorAttribute == null ) {
				_separatorAttribute = (SeparatorAttribute)attribute;
			}
			return _separatorAttribute;
		}
	}

	public override void OnGUI(Rect _position) {

		string title = separatorAttribute.title;
		float textGap = separatorAttribute.textGap;
		float borderOffset = separatorAttribute.borderOffset;

		_position.y += 19;
		float xMinOffset = _position.xMin + borderOffset;

		if ( title.Equals("") && textGap == 10.0 ) {
			_position.height = 1.0f;
			GUI.Box(new Rect(xMinOffset , _position.yMin , _position.width - borderOffset * 2 , 1.0f) , "");
		}
		else {
			float textLength = GUI.skin.label.CalcSize(new GUIContent(title)).x;
			float lineLength = ( _position.width - textLength - borderOffset * 2 - textGap * 2 ) / 2.0f;       
			float gapWithLength = lineLength + textGap;
			GUI.Box(new Rect(xMinOffset , _position.yMin , lineLength , 1.0f) , "");
			GUI.Label(new Rect(xMinOffset + gapWithLength , _position.yMin - 8.0f , textLength , EditorGUIUtility.singleLineHeight) , title);
			GUI.Box(new Rect(xMinOffset + gapWithLength + textLength + textGap , _position.yMin , lineLength , 1.0f) , "");
		}
	}

	public override float GetHeight() {
		return 40.0f;
	}
}

public class X <T, U> : Dictionary<T, U>
{
	
}