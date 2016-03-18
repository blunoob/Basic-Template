using System;
using UnityEngine;
using System.Collections;

public class Nonce {
	private static Guid g;
	
	public static string GetUniqueID(){
		g = Guid.NewGuid();
		
		
		return g.ToString();
    }
}