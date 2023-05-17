import { Color } from "./enums/color";
import { Schooling } from "./enums/schooling";
import { Region } from "./region";

export class Person {
    personId: string | undefined;
    name: string | undefined;
    document: number | undefined;
    documentFather: number | undefined;
    documentMother: number | undefined;
    color: Color | undefined;
    motherName: string | undefined;
    fatherName: string | undefined;
    schooling: Schooling | undefined;
    region: Region | undefined;
    createdAt: string | undefined;
    updatedAt: string | undefined;
}