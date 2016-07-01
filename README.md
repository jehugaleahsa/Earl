# Earl

Build URLs from RFC 6570 templates.

Download using NuGet: [Earl](http://nuget.org/packages/Earl)

## Overview
Earl allows you to define URI templates and build up complex URLs by substituting values for placeholders. Earl supports level 4 URI templates based on RFC 6570. This makes it a lot easier to build complex URLs, especially those containing embedded IDs, query strings, path segments (/), fragments (#) and other URI oddities. For example, here is a very complex URI that would be a pain to build otherwise:

    UriTemplate template = new UriTemplate("http://localhost{+port}/api{/version}/customers{?q,pagenum,pagesize}{#section}");
    string uri = template.Expand(new
    {
        port = ":8080",
        version = "v2",
        q = "rest",
        pagenum = 3,
        pagesize = (int?)null,
        section = "results"
    });

`UriTemplate` takes an object or `IDictionary` with properties/keys matching placeholer names in the template. It replaces the placeholders with the values in the data source. The template above will result in http://localhost:8080/api/v2/customers?q=rest&pagenum=3&pagesize=#results.

## License
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <http://unlicense.org>
