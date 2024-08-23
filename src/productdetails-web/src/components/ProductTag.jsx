const ProductTag = ({ tag }) => {
  return (
    <span className="p-1 mr-1 bg-yellow-400 rounded-sm text-xs font-bold">{tag.text}</span>
  )
}

export default ProductTag
