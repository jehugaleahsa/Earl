using System.Collections.Generic;
using Earl;
using Xunit;
using System;

namespace Earl.Tests
{
    public class UriTemplateTester
    {
        #region Level 1

        [Fact]
        public void ShouldExpandVariable()
        {
            UriTemplate template = new UriTemplate("{var}");
            string result = template.Expand(new { var = "value" });
            Assert.Equal("value", result);
        }

        [Fact]
        public void ShouldExpandGuid()
        {
            var guid = Guid.Parse("d3ae4104-d627-493e-a9fc-74b80b2fbd9a");
            UriTemplate template = new UriTemplate("{guid}");
            string result = template.Expand(new { guid = guid });
            Assert.Equal("d3ae4104-d627-493e-a9fc-74b80b2fbd9a", result);
        }
        [Fact]
        public void ShouldExpandDateTime()
        {
            var datetime = DateTimeOffset.Parse("2017-05-09T16:11:22.0000000+02:00");
            UriTemplate template = new UriTemplate("{datetime}");
            string result = template.Expand(new { datetime = datetime });
            Assert.Equal("2017-05-09T16%3A11%3A22.0000000%2B02%3A00", result);
        }
        [Fact]
        public void ShouldExpandDouble()
        {
            var @double = 10.458;
            UriTemplate template = new UriTemplate("{double}");
            string result = template.Expand(new { @double = @double });
            Assert.Equal("10.458", result);
        }
        [Fact]
        public void ShouldExpandFloat()
        {
            var @float = 10.458f;
            UriTemplate template = new UriTemplate("{float}");
            string result = template.Expand(new { @float = @float });
            Assert.Equal("10.458", result);
        }
        [Fact]
        public void ShouldExpandInt()
        {
            var @int = 10458;
            UriTemplate template = new UriTemplate("{int}");
            string result = template.Expand(new { @int = @int });
            Assert.Equal("10458", result);
        }

        [Fact]
        public void ShouldExpandVariableWithReservedCharacter()
        {
            UriTemplate template = new UriTemplate("{hello}");
            string result = template.Expand(new { hello = "Hello World!" });
            Assert.Equal("Hello+World!", result);
        }

        [Fact]
        public void ShouldExpandVariableDictionary()
        {
            UriTemplate template = new UriTemplate("{var}");
            string result = template.Expand(new Dictionary<string,object> { { "var", "value" } });
            Assert.Equal("value", result);
        }

        [Fact]
        public void ShouldExpandVariableWithReservedCharacterDictionary()
        {
            UriTemplate template = new UriTemplate("{hello}");
            string result = template.Expand(new Dictionary<string, object> { { "hello", "Hello World!" } });
            Assert.Equal("Hello+World!", result);
        }
        #endregion

        #region Level 2

        [Fact]
        public void ShouldExpandVariableWhenReservedAllowed()
        {
            UriTemplate template = new UriTemplate("{+var}");
            string result = template.Expand(new { var = "value" });
            Assert.Equal("value", result);
        }

        [Fact]
        public void ShouldExpandVariableWithSpaceWhenReservedAllowed()
        {
            UriTemplate template = new UriTemplate("{+hello}");
            string result = template.Expand(new { hello = "Hello, World!" });
            Assert.Equal("Hello,+World!", result);
        }

        [Fact]
        public void ShouldExpandVariableWithSlashWhenReservedAllowed()
        {
            UriTemplate template = new UriTemplate("{+path}/here");
            string result = template.Expand(new { path = "/foo/bar" });
            Assert.Equal("/foo/bar/here", result);
        }

        [Fact]
        public void ShouldExpandVariableInFragmentExpansion()
        {
            UriTemplate template = new UriTemplate("X{#var}");
            string result = template.Expand(new { var = "value" });
            Assert.Equal("X#value", result);
        }

        [Fact]
        public void ShouldExpandVariableWithReservedCharacterInFragmentExpansion()
        {
            UriTemplate template = new UriTemplate("X{#hello}");
            string result = template.Expand(new { hello = "Hello World!" });
            Assert.Equal("X#Hello+World!", result);
        }

        #endregion

        #region Level 3

        [Fact]
        public void ShouldExpandVariables()
        {
            UriTemplate template = new UriTemplate("map?{x,y}");
            string result = template.Expand(new { x = "1024", y = "768" });
            Assert.Equal("map?1024,768", result);
        }

        [Fact]
        public void ShouldExpandVariablesWithReservedCharacters()
        {
            UriTemplate template = new UriTemplate("{x,hello,y}");
            string result = template.Expand(new { x = "1024", hello = "Hello World!", y = 768 });
            Assert.Equal("1024,Hello+World!,768", result);
        }

        [Fact]
        public void ShouldExpandVariablesWithSpaceWhenReservedAllowed()
        {
            UriTemplate template = new UriTemplate("{+x,hello,y}");
            string result = template.Expand(new { x = 1024, hello = "Hello World!", y = 768 });
            Assert.Equal("1024,Hello+World!,768", result);
        }

        [Fact]
        public void ShouldExpandVariablesWithSlashWhenReservedAllowed()
        {
            UriTemplate template = new UriTemplate("{+path,x}/here");
            string result = template.Expand(new { x = 1024, path = "/foo/bar", y = 768 });
            Assert.Equal("/foo/bar,1024/here", result);
        }

        [Fact]
        public void ShouldExpandVariablesWithSpaceWhenReservedAllowedInFragmentExpansion()
        {
            UriTemplate template = new UriTemplate("{#x,hello,y}");
            string result = template.Expand(new { x = 1024, hello = "Hello World!", y = 768 });
            Assert.Equal("#1024,Hello+World!,768", result);
        }

        [Fact]
        public void ShouldExpandVariablesWithSlashWhenReservedAllowedInFragmentExpansion()
        {
            UriTemplate template = new UriTemplate("{#path,x}/here");
            string result = template.Expand(new { x = 1024, path = "/foo/bar", y = 768 });
            Assert.Equal("#/foo/bar,1024/here", result);
        }

        [Fact]
        public void ShouldExpandVariableInLabelExpansion()
        {
            UriTemplate template = new UriTemplate("X{.var}");
            string result = template.Expand(new { var = "value" });
            Assert.Equal("X.value", result);
        }

        [Fact]
        public void ShouldExpandVariablesInLabelExpansion()
        {
            UriTemplate template = new UriTemplate("X{.x,y}");
            string result = template.Expand(new { x = 1024, y = 768 });
            Assert.Equal("X.1024.768", result);
        }

        [Fact]
        public void ShouldExpandVariableInPathSegment()
        {
            UriTemplate template = new UriTemplate("{/var}");
            string result = template.Expand(new { var = "value" });
            Assert.Equal("/value", result);
        }

        [Fact]
        public void ShouldExpandVariablesInPathSegment()
        {
            UriTemplate template = new UriTemplate("{/var,x}/here");
            string result = template.Expand(new { var = "value", x = 1024 });
            Assert.Equal("/value/1024/here", result);
        }

        [Fact]
        public void ShouldExpandVariablesForPathStyleParameters()
        {
            UriTemplate template = new UriTemplate("{;x,y}");
            string result = template.Expand(new { x = 1024, y = 768 });
            Assert.Equal(";x=1024;y=768", result);
        }

        [Fact]
        public void ShouldExpandVariablesWithEmptyForPathStyleParameters()
        {
            UriTemplate template = new UriTemplate("{;x,y,empty}");
            string result = template.Expand(new { x = 1024, y = 768, empty = "" });
            Assert.Equal(";x=1024;y=768;empty", result);
        }

        [Fact]
        public void ShouldExpandVariablesInQuery()
        {
            UriTemplate template = new UriTemplate("{?x,y}");
            string result = template.Expand(new { x = 1024, y = 768 });
            Assert.Equal("?x=1024&y=768", result);
        }

        [Fact]
        public void ShouldExpandVariablesWithEmptyInQuery()
        {
            UriTemplate template = new UriTemplate("{?x,y,empty}");
            string result = template.Expand(new { x = 1024, y = 768, empty = "" });
            Assert.Equal("?x=1024&y=768&empty=", result);
        }

        [Fact]
        public void ShouldExpandVariablesInQueryContinuation()
        {
            UriTemplate template = new UriTemplate("?fixed=yes{&x}");
            string result = template.Expand(new { x = 1024 });
            Assert.Equal("?fixed=yes&x=1024", result);
        }

        [Fact]
        public void ShouldExpandVariablesWithEmptyInQueryContinuation()
        {
            UriTemplate template = new UriTemplate("{&x,y,empty}");
            string result = template.Expand(new { x = 1024, y = 768, empty = "" });
            Assert.Equal("&x=1024&y=768&empty=", result);
        }

        #endregion

        #region Level 4

        [Fact]
        public void ShouldExpandVariableWithSizeLimitSmallerThanValue()
        {
            UriTemplate template = new UriTemplate("{var:3}");
            string result = template.Expand(new { var = "value" });
            Assert.Equal("val", result);
        }

        [Fact]
        public void ShouldExpandVariableWithSizeLimitGreaterThanValue()
        {
            UriTemplate template = new UriTemplate("{var:30}");
            string result = template.Expand(new { var = "value" });
            Assert.Equal("value", result);
        }

        [Fact]
        public void ShouldExpandList()
        {
            UriTemplate template = new UriTemplate("{list}");
            string result = template.Expand(new { list = new string[] { "red", "green", "blue" } });
            Assert.Equal("red,green,blue", result);
        }

        [Fact]
        public void ShouldExpandListWhenExploded()
        {
            UriTemplate template = new UriTemplate("{list*}");
            string result = template.Expand(new { list = new List<string>() { "red", "green", "blue" } });
            Assert.Equal("red,green,blue", result);
        }

        [Fact]
        public void ShouldExpandPairs()
        {
            UriTemplate template = new UriTemplate("{keys}");
            string result = template.Expand(new 
            { 
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("semi,%3B,dot,.,comma,%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionary()
        {
            UriTemplate template = new UriTemplate("{keys}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("semi,%3B,dot,.,comma,%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsWhenExploded()
        {
            UriTemplate template = new UriTemplate("{keys*}");
            string result = template.Expand(new
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("semi=%3B,dot=.,comma=%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryWhenExploded()
        {
            UriTemplate template = new UriTemplate("{keys*}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("semi=%3B,dot=.,comma=%2C", result);
        }

        [Fact]
        public void ShouldExpandVariableWithSmallerSizeLimitWhenReservedAllowed()
        {
            UriTemplate template = new UriTemplate("{+path:6}/here");
            string result = template.Expand(new { path = "/foo/bar" });
            Assert.Equal("/foo/b/here", result);
        }

        [Fact]
        public void ShouldExpandListWhenReservedAllowed()
        {
            UriTemplate template = new UriTemplate("{+list}");
            string result = template.Expand(new { list = new HashSet<string>() { "red", "green", "blue" } });
            Assert.Equal("red,green,blue", result);
        }

        [Fact]
        public void ShouldExpandListWhenExplodedWhenReservedAllowed()
        {
            UriTemplate template = new UriTemplate("{+list*}");
            string result = template.Expand(new { list = new List<string>() { "red", "green", "blue" } });
            Assert.Equal("red,green,blue", result);
        }

        [Fact]
        public void ShouldExpandPairsWhenReservedAllowed()
        {
            UriTemplate template = new UriTemplate("{+keys}");
            string result = template.Expand(new
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("semi,;,dot,.,comma,,", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryWhenReservedAllowed()
        {
            UriTemplate template = new UriTemplate("{+keys}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("semi,;,dot,.,comma,,", result);
        }

        [Fact]
        public void ShouldExpandPairsWhenExplodedWhenReservedAllowed()
        {
            UriTemplate template = new UriTemplate("{+keys*}");
            string result = template.Expand(new
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("semi=;,dot=.,comma=,", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryWhenExplodedWhenReservedAllowed()
        {
            UriTemplate template = new UriTemplate("{+keys*}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("semi=;,dot=.,comma=,", result);
        }

        [Fact]
        public void ShouldExpandVariableWithSmallerSizeLimitInFragmentExpansion()
        {
            UriTemplate template = new UriTemplate("{#path:6}/here");
            string result = template.Expand(new { path = "/foo/bar" });
            Assert.Equal("#/foo/b/here", result);
        }

        [Fact]
        public void ShouldExpandListInFragmentExpansion()
        {
            UriTemplate template = new UriTemplate("{#list}");
            string result = template.Expand(new { list = new string[] { "red", "green", "blue" } });
            Assert.Equal("#red,green,blue", result);
        }

        [Fact]
        public void ShouldExpandListWhenExplodedInFragmentExpansion()
        {
            UriTemplate template = new UriTemplate("{#list*}");
            string result = template.Expand(new { list = new string[] { "red", "green", "blue" } });
            Assert.Equal("#red,green,blue", result);
        }

        [Fact]
        public void ShouldExpandPairsWhenInFragmentExpansion()
        {
            UriTemplate template = new UriTemplate("{#keys}");
            string result = template.Expand(new 
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("#semi,;,dot,.,comma,,", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryWhenInFragmentExpansion()
        {
            UriTemplate template = new UriTemplate("{#keys}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("#semi,;,dot,.,comma,,", result);
        }

        [Fact]
        public void ShouldExpandPairsWhenExplodedInFragmentExpansion()
        {
            UriTemplate template = new UriTemplate("{#keys*}");
            string result = template.Expand(new
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("#semi=;,dot=.,comma=,", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryWhenExplodedInFragmentExpansion()
        {
            UriTemplate template = new UriTemplate("{#keys*}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("#semi=;,dot=.,comma=,", result);
        }

        [Fact]
        public void ShouldExpandVariableWithSmallerSizeLimitInLabelExpansion()
        {
            UriTemplate template = new UriTemplate("X{.var:3}");
            string result = template.Expand(new { var = "value" });
            Assert.Equal("X.val", result);
        }

        [Fact]
        public void ShouldExpandListInLabelExpansion()
        {
            UriTemplate template = new UriTemplate("X{.list}");
            string result = template.Expand(new { list = new string[] { "red", "green", "blue" } });
            Assert.Equal("X.red,green,blue", result);
        }

        [Fact]
        public void ShouldExpandListWhenExplodedInLabelExpansion()
        {
            UriTemplate template = new UriTemplate("X{.list*}");
            string result = template.Expand(new { list = new string[] { "red", "green", "blue" } });
            Assert.Equal("X.red.green.blue", result);
        }

        [Fact]
        public void ShouldExpandPairsWhenInLabelExpansion()
        {
            UriTemplate template = new UriTemplate("X{.keys}");
            string result = template.Expand(new 
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("X.semi,%3B,dot,.,comma,%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryWhenInLabelExpansion()
        {
            UriTemplate template = new UriTemplate("X{.keys}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("X.semi,%3B,dot,.,comma,%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsWhenExplodedInLabelExpansion()
        {
            UriTemplate template = new UriTemplate("X{.keys*}");
            string result = template.Expand(new 
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("X.semi=%3B.dot=..comma=%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryWhenExplodedInLabelExpansion()
        {
            UriTemplate template = new UriTemplate("X{.keys*}");
            string result = template.Expand(new 
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("X.semi=%3B.dot=..comma=%2C", result);
        }

        [Fact]
        public void ShouldExpandVariableWithSizeLimitSmallerThanValueInPathSegment()
        {
            UriTemplate template = new UriTemplate("{/var:1,var}");
            string result = template.Expand(new { var = "value" });
            Assert.Equal("/v/value", result);
        }

        [Fact]
        public void ShouldExpandListWhenExplodedInPathSegment()
        {
            UriTemplate template = new UriTemplate("{/list}");
            string result = template.Expand(new { list = new string[] { "red", "green", "blue" } });
            Assert.Equal("/red,green,blue", result);
        }

        [Fact]
        public void ShouldExpandListAndVariableWithSizeLimiteInPathSegment()
        {
            UriTemplate template = new UriTemplate("{/list*,path:4}");
            string result = template.Expand(new 
            { 
                list = new string[] { "red", "green", "blue" },
                path = "/foo/bar"
            });
            Assert.Equal("/red/green/blue/%2Ffoo", result);
        }

        [Fact]
        public void ShouldExpandPairsInPathSegment()
        {
            UriTemplate template = new UriTemplate("{/keys}");
            string result = template.Expand(new
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("/semi,%3B,dot,.,comma,%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryInPathSegment()
        {
            UriTemplate template = new UriTemplate("{/keys}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("/semi,%3B,dot,.,comma,%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsWhenExplodedInPathSegment()
        {
            UriTemplate template = new UriTemplate("{/keys*}");
            string result = template.Expand(new 
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("/semi=%3B/dot=./comma=%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryWhenExplodedInPathSegment()
        {
            UriTemplate template = new UriTemplate("{/keys*}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("/semi=%3B/dot=./comma=%2C", result);
        }

        [Fact]
        public void ShouldExpandVariableWithSizeLimitSmallerThanValueForPathStyleParameters()
        {
            UriTemplate template = new UriTemplate("{;hello:5}");
            string result = template.Expand(new { hello = "Hello World!" });
            Assert.Equal(";hello=Hello", result);
        }

        [Fact]
        public void ShouldExpandListForPathStyleParameters()
        {
            UriTemplate template = new UriTemplate("{;list}");
            string result = template.Expand(new { list = new string[] { "red", "green", "blue" } });
            Assert.Equal(";list=red,green,blue", result);
        }

        [Fact]
        public void ShouldExpandListWhenExplodedForPathStyleParameters()
        {
            UriTemplate template = new UriTemplate("{;list*}");
            string result = template.Expand(new { list = new string[] { "red", "green", "blue" } });
            Assert.Equal(";list=red;list=green;list=blue", result);
        }

        [Fact]
        public void ShouldExpandPairsForPathStyleParameters()
        {
            UriTemplate template = new UriTemplate("{;keys}");
            string result = template.Expand(new 
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal(";keys=semi,%3B,dot,.,comma,%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryForPathStyleParameters()
        {
            UriTemplate template = new UriTemplate("{;keys}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal(";keys=semi,%3B,dot,.,comma,%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsWhenExplodedForPathStyleParameters()
        {
            UriTemplate template = new UriTemplate("{;keys*}");
            string result = template.Expand(new 
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal(";semi=%3B;dot=.;comma=%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryWhenExplodedForPathStyleParameters()
        {
            UriTemplate template = new UriTemplate("{;keys*}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal(";semi=%3B;dot=.;comma=%2C", result);
        }

        [Fact]
        public void ShouldExpandVariableWithSizeLimitSmallerThanValueInQuery()
        {
            UriTemplate template = new UriTemplate("{?var:3}");
            string result = template.Expand(new { var = "value" });
            Assert.Equal("?var=val", result);
        }

        [Fact]
        public void ShouldExpandListInQuery()
        {
            UriTemplate template = new UriTemplate("{?list}");
            string result = template.Expand(new { list = new string[] { "red", "green", "blue" } });
            Assert.Equal("?list=red,green,blue", result);
        }

        [Fact]
        public void ShouldExpandListWhenExplodedInQuery()
        {
            UriTemplate template = new UriTemplate("{?list*}");
            string result = template.Expand(new { list = new string[] { "red", "green", "blue" } });
            Assert.Equal("?list=red&list=green&list=blue", result);
        }

        [Fact]
        public void ShouldExpandPairsInQuery()
        {
            UriTemplate template = new UriTemplate("{?keys}");
            string result = template.Expand(new 
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("?keys=semi,%3B,dot,.,comma,%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryInQuery()
        {
            UriTemplate template = new UriTemplate("{?keys}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("?keys=semi,%3B,dot,.,comma,%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsWhenExplodedInQuery()
        {
            UriTemplate template = new UriTemplate("{?keys*}");
            string result = template.Expand(new 
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("?semi=%3B&dot=.&comma=%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryWhenExplodedInQuery()
        {
            UriTemplate template = new UriTemplate("{?keys*}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("?semi=%3B&dot=.&comma=%2C", result);
        }

        [Fact]
        public void ShouldExpandVariableWithSizeLimitSmallerThanValueInQueryContinuation()
        {
            UriTemplate template = new UriTemplate("{&var:3}");
            string result = template.Expand(new { var = "value" });
            Assert.Equal("&var=val", result);
        }

        [Fact]
        public void ShouldExpandListInQueryContinuation()
        {
            UriTemplate template = new UriTemplate("{&list}");
            string result = template.Expand(new { list = new string[] { "red", "green", "blue" } });
            Assert.Equal("&list=red,green,blue", result);
        }

        [Fact]
        public void ShouldExpandListWhenExplodedInQueryContinuation()
        {
            UriTemplate template = new UriTemplate("{&list*}");
            string result = template.Expand(new { list = new string[] { "red", "green", "blue" } });
            Assert.Equal("&list=red&list=green&list=blue", result);
        }

        [Fact]
        public void ShouldExpandPairsInQueryContinuation()
        {
            UriTemplate template = new UriTemplate("{&keys}");
            string result = template.Expand(new 
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("&keys=semi,%3B,dot,.,comma,%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryInQueryContinuation()
        {
            UriTemplate template = new UriTemplate("{&keys}");
            string result = template.Expand(new
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("&keys=semi,%3B,dot,.,comma,%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsWhenExplodedInQueryContinuation()
        {
            UriTemplate template = new UriTemplate("{&keys*}");
            string result = template.Expand(new
            {
                keys = new
                {
                    semi = ";",
                    dot = ".",
                    comma = ","
                }
            });
            Assert.Equal("&semi=%3B&dot=.&comma=%2C", result);
        }

        [Fact]
        public void ShouldExpandPairsAsDictionaryWhenExplodedInQueryContinuation()
        {
            UriTemplate template = new UriTemplate("{&keys*}");
            string result = template.Expand(new 
            {
                keys = new Dictionary<string, string>()
                {
                    { "semi", ";" },
                    { "dot", "." },
                    { "comma", "," }
                }
            });
            Assert.Equal("&semi=%3B&dot=.&comma=%2C", result);
        }

        #endregion

        #region Examples

        [Fact]
        public void ShouldHandleMultipleSubstitutions1()
        {
            UriTemplate template = new UriTemplate("http://localhost{+port}/api{/version}/customers{?q,pagenum,pagesize,start}{#section}");
            string uri = template.Expand(new
            {
                port = ":8080",
                version = "v2",
                q = "rest",
                pagenum = 3,
                pagesize = (int?)null,
                section = "results",
                start = new DateTime(2017, 05, 09, 16, 11, 22, 0, DateTimeKind.Local)
            });
            Assert.Equal(uri = "http://localhost:8080/api/v2/customers?q=rest&pagenum=3&pagesize=&start=2017-05-09T16%3A11%3A22.0000000%2B02%3A00#results", uri);
        }

        #endregion
    }
}
