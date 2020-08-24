using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Kogane.Internal
{
	public sealed class PrefabLayoutGroupTest
	{
		private sealed class TestResult
		{
			private readonly string m_path;
			private readonly string m_gameObjectHierarchyPath;

			public TestResult( GameObject gameObject )
			{
				m_path                    = string.Empty; // TODO:
				m_gameObjectHierarchyPath = GetHierarchyPath( gameObject );
			}

			public void WriteTo( StringBuilder builder )
			{
				builder.Append( m_path );
				builder.Append( ',' );
				builder.Append( m_gameObjectHierarchyPath );
				builder.AppendLine();
			}

			private static string GetHierarchyPath( GameObject gameObject )
			{
				var path   = gameObject.name;
				var parent = gameObject.transform.parent;

				while ( parent != null )
				{
					path   = parent.name + "/" + path;
					parent = parent.parent;
				}

				return path;
			}
		}

		[Test]
		public void Test()
		{
			var results = new List<TestResult>();

			GameObjectProcessor.ProcessAllPrefabs
			(
				prefabPathFilter: path => path.StartsWith( "Assets" ),
				onProcess: gameObject =>
				{
					var layoutGroup       = gameObject.GetComponent<LayoutGroup>();
					var contentSizeFitter = gameObject.GetComponent<ContentSizeFitter>();

					if ( layoutGroup == null ) return GameObjectProcessResult.NOT_CHANGE;
					if ( contentSizeFitter == null ) return GameObjectProcessResult.NOT_CHANGE;
					if ( !layoutGroup.enabled && !contentSizeFitter.enabled ) return GameObjectProcessResult.NOT_CHANGE;

					var result = new TestResult( gameObject );

					results.Add( result );

					return GameObjectProcessResult.NOT_CHANGE;
				}
			);

			if ( results.Count <= 0 ) return;

			var builder = new StringBuilder();

			foreach ( var result in results )
			{
				result.WriteTo( builder );
			}

			Assert.Fail( builder.ToString().TrimEnd() );
		}
	}
}