using PropertyTenants.Domain.Entities.Directory;

namespace PropertyTenants.Gateways.GraphQL.Types.ObjectTypes;

[ObjectType]
public class FileEntryType : ObjectType<FileEntry>
{
    protected override void Configure(IObjectTypeDescriptor<FileEntry> descriptor)
    {
        descriptor
            .Name("FileEntry")
            .Description("A file entry in the system");

        descriptor
            .Field(f => f.Id)
            .Description("The unique identifier")
            .Type<NonNullType<UuidType>>();

        descriptor
            .Field(f => f.Name)
            .Description("The name of the file")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.Description)
            .Description("The description of the file")
            .Type<StringType>();

        descriptor
            .Field(f => f.Size)
            .Description("The size of the file in bytes")
            .Type<NonNullType<LongType>>();

        descriptor
            .Field(f => f.ContentType)
            .Description("The content type of the file")
            .Type<StringType>();

        descriptor
            .Field(f => f.FileName)
            .Description("The original filename")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.FileLocation)
            .Description("The location where the file is stored")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.IsEncrypted)
            .Description("Whether the file is encrypted")
            .Type<NonNullType<BooleanType>>();

        descriptor
            .Field(f => f.CreatedAt)
            .Description("When the file was uploaded")
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(f => f.LastUpdatedAt)
            .Description("When the file was last updated")
            .Type<DateTimeType>();

        // Hide sensitive encryption fields
        descriptor
            .Field(f => f.EncryptionKey)
            .Ignore();

        descriptor
            .Field(f => f.EncryptionIV)
            .Ignore();

        // Computed fields
        descriptor
            .Field("sizeFormatted")
            .Description("Human readable file size")
            .Type<StringType>()
            .Resolve(context =>
            {
                var fileEntry = context.Parent<FileEntry>();
                return FormatFileSize(fileEntry.Size);
            });
    }

    private static string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }
}
