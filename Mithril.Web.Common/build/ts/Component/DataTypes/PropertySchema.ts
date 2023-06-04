import Metadata from "./MetadataSchema";

// Basic schema describing a property
export default class PropertySchema {
    // Display name of the property
    public displayName: string;

    // Property name (name of the property in the model object)
    public propertyName: string;

    // Property type (component used to display it)
    public propertyType: string;

    // Property metadata
    public metadata: Metadata;
}