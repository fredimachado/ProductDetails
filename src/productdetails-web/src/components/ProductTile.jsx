import ProductPrice from "./ProductPrice";
import ProductTag from "./ProductTag"
import ProductTagTop from "./ProductTagTop";

const ProductTile = ({ product }) => {
  function getMiddleTags(tags) {
    const categories = ['Save'];
    return tags.filter(t => categories.includes(t.category));
  }
  function getTopTags(tags) {
    const categories = ['BestSeller', 'FlashDeal'];
    return tags.filter(t => categories.includes(t.category));
  }
  return (
    <div className="bg-white relative rounded-md ring-1 ring-slate-500/10">
        <div>
          <div className="absolute right-0" style={{ top: '2px' }}>
            {getTopTags(product.tags).map((tag, index) =>
              <ProductTagTop key={index} tag={tag} />
            )}
          </div>
          <img src={`images/products/${product.image}-sm.png`} className="card-img-top mx-auto" alt="..." />
          <div className="h-6 pl-3 pr-3 relative -top-3">
            {getMiddleTags(product.tags).map((tag, index) =>
              <ProductTag key={index} tag={tag} />
            )}
          </div>
          <div className="pl-3 pr-3 pb-3">
              <ProductPrice price={product.price} wasPrice={product.wasPrice} />
              <h5>{product.name}</h5>
              {/* <button type="button" className="w-full flex-auto rounded-md mt-3 px-4 py-2 text-center font-medium ring-1 ring-slate-700/10 hover:bg-slate-50">Add</button> */}
          </div>
        </div>
    </div>
  )
}

export default ProductTile
