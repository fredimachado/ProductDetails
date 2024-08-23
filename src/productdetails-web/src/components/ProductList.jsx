import ProductTile from "./ProductTile";
import { useQuery, gql } from "@apollo/client";
import { loadErrorMessages, loadDevMessages } from "@apollo/client/dev";

loadDevMessages();
loadErrorMessages();
  
const PRODUCTS_QUERY = gql`
  {
    products {
      description
      image
      name
      price
      wasPrice
      stockcode
      tags {
        category
        kind
        text
        value
      }
    }
  }
`;


const ProductList = () => {
  const { data, loading, error } = useQuery(PRODUCTS_QUERY);

  if (loading) return "Loading...";
  if (error) return <pre>{error.message}</pre>

  return (
    <section className="px-4 py-10">
      <div className="container-xl lg:container m-auto">
        <div className="grid grid-cols-1 sm:grid-cols-3 lg:grid-cols-6 md:grid-cols-4 gap-4">
        {data.products.map((product) =>
          <ProductTile key={product.stockcode} product={product} />
        )}
        </div>
      </div>
    </section>
  )
}

export default ProductList
