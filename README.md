# Covéa Developer Technical Challenge

<sup>
1 hour // Node.js, React, C#
</sup>

## Getting Started
Fork this repo and clone locally, initialise the project as you normally would, and build the application according to the brief below.

When completed, update this README with instructions on how to run and test your code, push the changes to your own fork, and send a link to us so we can take a look!

## The Brief
Below is a product brief for a simple Pet Insurance API that allows customers to get insurance quotes for their pets. You must build as much of the application as you can within the allotted time. Please focus on test coverage.

## User Story
As a customer I would like to be able to get an insurance quote by providing some details about my pet(s).

In the response I should get a monthly premium quote for each pet I enquire about, including a total.

If my pet is not ‘valid’ it is uninsurable.

### Diagram:

![a0567f9c-ad48-4314-b77b-2d939461691d](https://user-images.githubusercontent.com/1726083/123427367-71845d00-d5bc-11eb-9185-be588ca18d57.png)


### Example Request:

![5e4985b6-2b98-4c4c-8972-db8ed4e5a041](https://user-images.githubusercontent.com/1726083/123427380-747f4d80-d5bc-11eb-8737-4c025a229d87.png)


### Example Response:

![117f7af5-f9b3-4129-8c7e-a332983f7622](https://user-images.githubusercontent.com/1726083/123427395-7812d480-d5bc-11eb-935e-f488a1ac852e.png)

 

## Requirements
When provided with information (breed, age, sum assured) the API should return the correct quote.

Susan’s 7 year old Pomeranian should have a monthly insurance premium of £31.53 with a sum assured of £4000

Helen’s 4 year old Burmese cat should have a monthly insurance premium of £43.55 with a sum assured of £8000

Simon’s 6 year old Miniature Poodle should have a monthly insurance premium of £31.20 with a sum assured of £1000, and his 9 year old Abyssinian should have a monthly insurance premium of £41.93 with a sum assured of £4000

Adam’s 11 year old American Pit Bull Terrier should be uninsurable

 

## Additional Information
Insurance premiums are calculated as follows:

```
Breed Loading = Breed group rating * (Sum assured / 1000)

Age Loading = 1 + Age * 110%

Net premium = Age Loading + Breed Loading

Initial commission = Net premium * 205%

IPT = Net premium * 20%

Monthly Gross premium = Net premium + Initial commission + IPT
```
