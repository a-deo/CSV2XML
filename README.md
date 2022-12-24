# CSV2XML

## Description
CSV2XML is an application that can load CSV files, parse its content and generate a XML structure. The XML structure is based on a custom structure and may need to be customised for personal use depending on your requirements. 

Here is the default XML structure for CSV2XML:
```
<root>
  <attribute attributeName1="Value" attributeName2="123" attributeName3="TRUE">
    <description>Lorem ipsum</description>
  </attribute>
</root>
```

## Installation: 
#### Instructions for how to install and set up the project, including any dependencies that need to be installed.
CSV2XML is ready to be compiled when cloned and comes with a precompiled .EXE file. For best practice, I recommend reviewing the code and compiling the project yourself. 

MD5 hash of the pre-compiled .EXE file is: ``` 4857d1a8ef1bff56a77b32b5797486d3 ```

## Usage: 
#### A guide to using the project, including any command-line arguments or configuration options.
CSV2XML is a fairly simple application. The main user experience can be summarised with 2 steps:
1. Load a CSV file.
2. Save or Copy the generated XML.

## Examples: 
#### Examples of how to use the project, with code snippets and/or screenshots.
If the default XML structure of CSV2XML doesn't match your current XML requirements, you can edit the `MainWindow.xaml.cs` file. 

For example, instead of using CSV2XML's default XML structure: 
```
<root>
  <attribute attributeName1="Value" attributeName2="123" attributeName3="TRUE">
    <description>Lorem ipsum</description>
  </attribute>
</root>
```

Let's implement a different structure. Dynamically, create the following: a static XElement, an XAttribute on the 1st column, and Elements on the 2nd, 3rd, and 4th columns:
```
<root>
  <element attribute="FALSE">
    <element0>First Name</element0>
    <element1>Last Name</element1>
    <element2>Phone Number</element2>
  </element>
</root>
```

The code to implement this new structure would be the following:
```
XDocument doc = new XDocument(
                new XElement("root",
                    // Loop through the CSV file lines
                    lines.Skip(1).Select(line => {
                        // Split the line into fields
                        string[] fields = line.Split(',');

                        // Create the XML element
                        return new XElement("element",
                            // Set the first field as an attribute of the element
                            new XAttribute("attribute", fields[0]),
                            // Loop through the remaining fields and create nested elements
                            fields.Skip(1).Select((value, index) => {
                                // Create the element name by combining "element" and the index
                                string elementName = "element" + index;
                                // Only return the XElement if the field value is not empty
                                return value != "" ? new XElement(elementName, value) : null;
                            })
                        );
                    })
                )
            );
```

I have provided 3 different CSV files as examples to generate XML. Purpose of each file:
1. ```example.csv```: Generic CSV example with values.
2. ```example2.csv```: Similar to the first file, but doesn't provide values for each attribute to demonstrate the attribute not generating when parsed.
3. ```example3.csv```: CSV example for the modified XML structure above.

## Contributing: 
#### Information on how to contribute to the project, including any guidelines or code of conduct.
If you'd like to contribute to this project, please either:
1. Fork the project. Make a new branch with your changes. Create a pull request. 
2. Raise an issue. 

## License: 
#### Information on the license under which the project is released.
This project is released under the Apache Licence 2.0. For more info, please visit this [link](https://www.apache.org/licenses/LICENSE-2.0), or for a summarised version, visit this [link](https://choosealicense.com/licenses/apache-2.0/).

## Contact: 
#### A way for users to get in touch with the maintainers of the project.
To get in contact, please reach out to my email found on my Github account.

Thanks for checking out the README, have a great day! :)