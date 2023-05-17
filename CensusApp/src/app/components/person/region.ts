import { Continent } from "./enums/continent";

export class Region {
    continent: Continent | undefined;
    zipCode: number | undefined;
    street: string | undefined;
    neighborhood: string | undefined;
    city: string | undefined;
    state: string | undefined;
    country: string | undefined;
}