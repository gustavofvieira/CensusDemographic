import { Person } from "./person";

export class FamilyTree {
    person: Person | undefined;
    parents: FamilyTree[] | undefined;
}